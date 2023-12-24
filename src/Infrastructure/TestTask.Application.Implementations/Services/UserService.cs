using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TestTask.Application.Contracts;
using TestTask.Application.Contracts.Common;
using TestTask.Application.Implementations.Extensions;
using TestTask.Application.Implementations.Extensions.Extensions;
using TestTask.Application.Services;
using TestTask.DAL;
using TestTask.Domain.Entities;

namespace TestTask.Application.Implementations.Services;

internal class UserService(
	TestTaskDbContext dbContext, 
	IValidator<PagingOptions> pagingOptionsValidator, 
	IValidator<UsersFilteringOptions> filteringOptionsValidator, 
	IFilteringOptionsChecker<UsersFilteringOptions> filteringOptionsChecker) : IUserService
{
	private readonly TestTaskDbContext _dbContext = dbContext;
	private readonly IValidator<PagingOptions> _pagingOptionsValidator = pagingOptionsValidator;
	private readonly IValidator<UsersFilteringOptions> _filteringOptionsValidator = filteringOptionsValidator;
	private readonly IFilteringOptionsChecker<UsersFilteringOptions> _filteringOptionsChecker = filteringOptionsChecker;

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

	public Task<Result<UserDTO>> GetByIdAsync(UserId userId, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public Task<Result> RegisterAsync(UserRegisterDTO userRegisterDTO, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public Task<Result> UpdateAsync(UserUpdateDTO userUpdateDTO, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}
}
