using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TestTask.Application.Contracts;
using TestTask.Application.Contracts.Common;
using TestTask.Application.Implementations.Extensions;
using TestTask.Application.Implementations.Extensions.Extensions;
using TestTask.Application.Services;
using TestTask.DAL;
using TestTask.Domain.Constants;
using TestTask.Domain.Entities;

namespace TestTask.Application.Implementations.Services;

internal class RoleService(TestTaskDbContext dbContext, IValidator<PagingOptions> pagingOptionsValidator, IValidator<UserAddToRoleDTO> addToRoleValidator) : IRoleService
{
	private readonly TestTaskDbContext _dbContext = dbContext;
	private readonly IValidator<PagingOptions> _pagingOptionsValidator = pagingOptionsValidator;
	private readonly IValidator<UserAddToRoleDTO> _addToRoleValidator = addToRoleValidator;

	public async Task<Result<RolesPage>> GetAsync(PagingOptions? pagingOptions = null, CancellationToken cancellationToken = default)
	{
		if (pagingOptions is not null)
		{
			var validationResult = _pagingOptionsValidator.Validate(pagingOptions);
			if (!validationResult.IsValid)
			{
				return Result.Failure<RolesPage>(validationResult.ToString());
			}
		}

		int totalCount = _dbContext.Roles.Count();
		var roles = await _dbContext
			.Roles
			.OrderBy(e => e.Title)
			.ApplyPaging(pagingOptions)
			.Select(e => e.ToDTO())
			.ToListAsync(cancellationToken);

		return new RolesPage(roles, totalCount, pagingOptions);
	}

	public async Task<Result> AddToRoleAsync(UserId requesterId, UserAddToRoleDTO userAddToRoleDTO, CancellationToken cancellationToken = default)
	{
		var validationResult = _addToRoleValidator.Validate(userAddToRoleDTO);
		if (!validationResult.IsValid)
		{
			return Result.Failure(validationResult.ToString());
		}

		if (requesterId == userAddToRoleDTO.ToId)
		{
			return Result.Failure("You can't add new role to yourself.");
		}

		var requester = await _dbContext
			.Users
			.Include(e => e.Roles)
			.ThenInclude(e => e.Role)
			.SingleOrDefaultAsync(e => e.Id == requesterId, cancellationToken);

		if (requester is null)
		{
			return Result.Failure("Requester not found.");
		}

		var role = await _dbContext.Roles.SingleOrDefaultAsync(e => e.Id == userAddToRoleDTO.RoleId, cancellationToken);
		if (role is null)
		{
			return Result.Failure(Errors.EntityWithPassedIdIsNotExists(nameof(Role)));
		}

		var user = await _dbContext
			.Users
			.Include(e => e.Roles)
			.ThenInclude(e => e.Role)
			.SingleOrDefaultAsync(e => e.Id == userAddToRoleDTO.ToId, cancellationToken);

		if (user is null)
		{
			return Result.Failure(Errors.EntityWithPassedIdIsNotExists(nameof(User)));
		}

		if (user.IsInRole(role.Id))
		{
			return Result.Failure("User is already in role.");
		}

		var actionPermitted = requester.IsInRole(Roles.SuperAdmin);
		if (!actionPermitted)
		{
			return Result.Failure(Errors.Auth.AccessDenided);
		}

		user.Roles.Add(new UserRole
		{
			RoleId = role.Id,
			UserId = user.Id,
		});

		await _dbContext.SaveChangesAsync(cancellationToken);
		return Result.Success();
	}
}