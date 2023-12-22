using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TestTask.Application.Responses;
using TestTask.Application.Services;
using TestTask.DAL;
using TestTask.Domain.Entities;

namespace TestTask.Application.Implementations.Services;

internal class UserService(TestTaskDbContext dbContext, IValidator<PagingOptions> pagingOptionsValidator) : IUserService
{
	private readonly TestTaskDbContext _dbContext = dbContext;
	private readonly IValidator<PagingOptions> _pagingOptionsValidator = pagingOptionsValidator;

	public async Task<Result<UsersPage>> GetAsync(
		UsersSortingOptions sortingOptions,
		PagingOptions? pagingOptions = null,
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

		int totalItemsCount = _dbContext.Users.Count();
		var result = await _dbContext
			.Users
			.Include(e => e.Roles)
			.ThenInclude(e => e.Role)
			.AsNoTracking()
			.Sort(sortingOptions)
			.Page(pagingOptions)
			.Select(e => e.ToDTO())
			.ToListAsync(cancellationToken);

		return new UsersPage(result, totalItemsCount, sortingOptions, pagingOptions);
	}

	public Task<Result> AddRoleAsync(UserAddRoleDTO addRoleToUserDTO, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
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
