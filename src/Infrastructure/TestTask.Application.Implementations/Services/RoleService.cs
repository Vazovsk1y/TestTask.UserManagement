using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TestTask.Application.Contracts;
using TestTask.Application.Contracts.Common;
using TestTask.Application.Implementations.Extensions;
using TestTask.Application.Implementations.Extensions.Extensions;
using TestTask.Application.Services;
using TestTask.DAL;

namespace TestTask.Application.Implementations.Services;

internal class RoleService(TestTaskDbContext dbContext, IValidator<PagingOptions> pagingOptionsValidator) : IRoleService
{
	private readonly TestTaskDbContext _dbContext = dbContext;
	private readonly IValidator<PagingOptions> _pagingOptionsValidator = pagingOptionsValidator;

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

	public Task<Result> SetRoleAsync(UserSetRoleDTO userSetRoleDTO, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}
}