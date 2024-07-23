using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MikesPaging.AspNetCore.Common;
using MikesPaging.AspNetCore.Services.Interfaces;
using TestTask.Application.Contracts;
using TestTask.Application.Implementations.Constants;
using TestTask.Application.Implementations.Extensions;
using TestTask.DAL;
using TestTask.Domain.Constants;
using TestTask.Domain.Entities;
using TestTask.Domain.Shared;

namespace TestTask.Application.Implementations;

internal class UserService(
	TestTaskDbContext dbContext,
	IValidator<UserRegisterDTO> registerDtoValidator,
	IValidator<UserUpdateDTO> updateDtoValidator,
	IPagingManager<User> pagingManager,
	ISortingManager<User> sortingManager,
	IFilteringManager<User> filteringManager) : IUserService
{
	public async Task<Result<UsersPage>> GetPageAsync(
		UserId requesterId,
		SortingOptions<UsersSortingEnum> sortingOptions,
		PagingOptions? pagingOptions = null,
		FilteringOptions<UsersFilteringEnum>? filteringOptions = null,
		CancellationToken cancellationToken = default)
	{
		var user = await dbContext
			.Users
			.Include(e => e.Roles)
			.ThenInclude(e => e.Role)
			.SingleOrDefaultAsync(e => e.Id == requesterId, cancellationToken);

		if (user is null)
		{
			return Result.Failure<UsersPage>(("UserService.RequesterNotFound", "Requester not found."));
		}

		var isActionPermitted = user.IsInRole(Roles.SuperAdmin) || user.IsInRole(Roles.Admin) || user.IsInRole(Roles.Support);
		if (!isActionPermitted)
		{
			return Result.Failure<UsersPage>(Errors.Auth.AccessDenied);
		}

		int totalUsersCount = filteringManager.ApplyFiltering(dbContext.Users.AsQueryable(), filteringOptions).Count();
		var query = dbContext
			.Users
			.Include(e => e.Roles)
			.ThenInclude(e => e.Role)
			.AsNoTracking();

		query = filteringManager.ApplyFiltering(query, filteringOptions);
		query = sortingManager.ApplySorting(query, sortingOptions);
		query = pagingManager.ApplyPaging(query, pagingOptions);

		var users = await query.Select(e => e.ToDTO()).ToListAsync(cancellationToken);
		
		return new UsersPage(
			users,
			totalUsersCount,
			sortingOptions,
			filteringOptions,
			pagingOptions);
	}

	public async Task<Result<UserDTO>> GetByIdAsync(UserId requesterId, UserId targetId, CancellationToken cancellationToken = default)
	{
		var requester = await dbContext
			.Users
			.Include(e => e.Roles)
			.ThenInclude(e => e.Role)
			.SingleOrDefaultAsync(e => e.Id == requesterId, cancellationToken);

		if (requester is null)
		{
			return Result.Failure<UserDTO>(("UserService.RequesterNotFound", "Requester not found."));
		}

		if (requester.Id == targetId)
		{
			return requester.ToDTO();
		}

		var actionPermitted = requester.IsInRole(Roles.Support) || requester.IsInRole(Roles.Admin) || requester.IsInRole(Roles.SuperAdmin);
		if (!actionPermitted)
		{
			return Result.Failure<UserDTO>(Errors.Auth.AccessDenied);
		}

		var searchUser = await dbContext
			.Users
			.Include(e => e.Roles)
			.ThenInclude(e => e.Role)
			.SingleOrDefaultAsync(e => e.Id == targetId, cancellationToken);

		return searchUser?.ToDTO() ?? Result.Failure<UserDTO>(("UserService.UserNotFound", "Target user not found."));
	}

	public async Task<Result<UserId>> RegisterAsync(UserRegisterDTO userRegisterDTO, CancellationToken cancellationToken = default)
	{
		var validationResult = await registerDtoValidator.ValidateAsync(userRegisterDTO, cancellationToken);
		if (!validationResult.IsValid)
		{
			return Result.Failure<UserId>(validationResult.Errors.ToSharedErrors());
		}

		if (await dbContext.Users.IsEmailTakenAsync(userRegisterDTO.Credentials.Email))
		{
			return Result.Failure<UserId>(Errors.User.EmailIsAlreadyTaken);
		}

		string passwordHash = BCrypt.Net.BCrypt.HashPassword(userRegisterDTO.Credentials.Password);
		var user = new User
		{
			Age = userRegisterDTO.Age,
			FullName = userRegisterDTO.FullName,
			Email = userRegisterDTO.Credentials.Email,
			PasswordHash = passwordHash
		};

		var defaultRole = await dbContext.Roles.GetRoleByTitleAsync(Roles.User);
		user.Roles.Add(new UserRole { RoleId = defaultRole.Id, UserId = user.Id });

		dbContext.Users.Add(user);
		await dbContext.SaveChangesAsync(cancellationToken);
		return user.Id;
	}

	public async Task<Result> UpdateAsync(UserId requesterId, UserUpdateDTO userUpdateDTO, CancellationToken cancellationToken = default)
	{
		var targetId = new UserId(userUpdateDTO.Id);
		var validationResult = await updateDtoValidator.ValidateAsync(userUpdateDTO, cancellationToken);
		if (!validationResult.IsValid)
		{
			return Result.Failure(validationResult.Errors.ToSharedErrors());
		}

		var requester = await dbContext
			.Users
			.Include(e => e.Roles)
			.ThenInclude(e => e.Role)
			.SingleOrDefaultAsync(e => e.Id == requesterId, cancellationToken);

		if (requester is null)
		{
			return Result.Failure<UserDTO>(("UserService.RequesterNotFound", "Requester not found."));
		}

		var actionPermitted = requester.IsInRole(Roles.Admin) || requester.IsInRole(Roles.SuperAdmin) || requester.Id == targetId;
		if (!actionPermitted)
		{
			return Result.Failure(Errors.Auth.AccessDenied);
		}

		if (requester.Id == targetId)
		{
			requester.Age = userUpdateDTO.Age;
			requester.FullName = userUpdateDTO.FullName;
			await dbContext.SaveChangesAsync(cancellationToken);
			return Result.Success();
		}

		var user = await dbContext
			.Users
			.Include(e => e.Roles)
			.ThenInclude(e => e.Role)
			.SingleOrDefaultAsync(e => e.Id == targetId, cancellationToken);

		if (user is null)
		{
			return Result.Failure<UserDTO>(("UserService.UserNotFound", "Target user not found."));
		}

		user.Age = userUpdateDTO.Age;
		user.FullName = userUpdateDTO.FullName;
		await dbContext.SaveChangesAsync(cancellationToken);
		return Result.Success();
	}
}
