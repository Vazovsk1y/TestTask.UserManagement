using Microsoft.EntityFrameworkCore;
using TestTask.Application.Contracts;
using TestTask.Application.Contracts.Common;
using TestTask.Application.Implementations.Extensions.Extensions;
using TestTask.Application.Services;
using TestTask.DAL;

namespace TestTask.Application.Implementations.Services;

internal class RoleService(TestTaskDbContext dbContext) : IRoleService
{
	private readonly TestTaskDbContext _dbContext = dbContext;

	public async Task<Result<IReadOnlyCollection<RoleDTO>>> GetAllAsync(CancellationToken cancellationToken = default)
	{
		return await _dbContext.Roles.Select(e => e.ToDTO()).ToListAsync(cancellationToken);
	}

	public Task<Result> SetRoleAsync(UserSetRoleDTO userSetRoleDTO, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}
}