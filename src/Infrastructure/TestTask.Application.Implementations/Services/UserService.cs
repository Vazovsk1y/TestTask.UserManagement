using Azure;
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

internal class UserService(
	TestTaskDbContext dbContext,
	IValidator<PagingOptions> pagingOptionsValidator,
	IValidator<UsersFilteringOptions> filteringOptionsValidator,
	IFilteringOptionsChecker<UsersFilteringOptions> filteringOptionsChecker,
	IValidator<UserRegisterDTO> registerDtoValidator) : IUserService
{
	private readonly TestTaskDbContext _dbContext = dbContext;
	private readonly IValidator<PagingOptions> _pagingOptionsValidator = pagingOptionsValidator;
	private readonly IValidator<UsersFilteringOptions> _filteringOptionsValidator = filteringOptionsValidator;
	private readonly IFilteringOptionsChecker<UsersFilteringOptions> _filteringOptionsChecker = filteringOptionsChecker;
	private readonly IValidator<UserRegisterDTO> _registerDtoValidator = registerDtoValidator;

	public async Task<Result<UsersPage>> GetAsync(
		UsersSortingOptions sortingOptions,
		PagingOptions? pagingOptions = null,
		UsersFilteringOptions? filteringOptions = null,
		CancellationToken cancellationToken = default)
	{
		if (pagingOptions is not null)
		{
			var validationResult = _pagingOptionsValidator.Validate(pagingOptions);
			if (!validationResult.IsValid)
			{
				return Result.Failure<UsersPage>(validationResult.ToString());
			}
		}

		if (filteringOptions is not null)
		{
			var validationResult = _filteringOptionsValidator.Validate(filteringOptions);
			if (!validationResult.IsValid)
			{
				return Result.Failure<UsersPage>(validationResult.ToString());
			}

			var applicabilityResult = _filteringOptionsChecker.IsAppliсable(filteringOptions);
			if (applicabilityResult.IsFailure)
			{
				return Result.Failure<UsersPage>(applicabilityResult.ErrorMessage);
			}
		}

		int totalUsersCount = _dbContext.Users.ApplyFiltering(filteringOptions).Count();

		var users = await _dbContext
			.Users
			.Include(e => e.Roles)
			.ThenInclude(e => e.Role)
			.AsNoTracking()
			.ApplyFiltering(filteringOptions)
			.ApplySorting(sortingOptions)
			.ApplyPaging(pagingOptions)
			.Select(e => e.ToDTO())
			.ToListAsync(cancellationToken);

		return new UsersPage(
			users,
			totalUsersCount,
			sortingOptions,
			pagingOptions,
			filteringOptions);
	}

	public async Task<Result<UserDTO>> GetByIdAsync(UserId userId, CancellationToken cancellationToken = default)
	{
		var user = await _dbContext.Users
			.Include(e => e.Roles)
			.ThenInclude(e => e.Role)
			.SingleOrDefaultAsync(e => e.Id == userId, cancellationToken);

		if (user is null)
		{
			return Result.Failure<UserDTO>(Errors.EntityWithPassedIdIsNotExists(nameof(User)));
		}

		return user.ToDTO();
	}

	public async Task<Result<UserId>> RegisterAsync(UserRegisterDTO userRegisterDTO, CancellationToken cancellationToken = default)
	{
		var validationResult = _registerDtoValidator.Validate(userRegisterDTO);
		if (!validationResult.IsValid)
		{
			return Result.Failure<UserId>(validationResult.ToString());
		}

		if (await _dbContext.Users.IsEmailTakenAsync(userRegisterDTO.Credentials.Email))
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

		var defaultRole = await _dbContext.Roles.GetRoleByTitleAsync(Roles.User);
		user.Roles.Add(new UserRole { RoleId = defaultRole.Id, UserId = user.Id });

		_dbContext.Users.Add(user);
		await _dbContext.SaveChangesAsync(cancellationToken);
		return user.Id;
	}

	public Task<Result> UpdateAsync(UserUpdateDTO userUpdateDTO, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}
}
