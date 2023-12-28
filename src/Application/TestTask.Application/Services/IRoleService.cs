using TestTask.Application.Contracts;
using TestTask.Application.Contracts.Common;
using TestTask.Domain.Entities;

namespace TestTask.Application.Services;

public interface IRoleService
{
	Task<Result> AddToRoleAsync(UserId requesterId, UserAddToRoleDTO userAddToRoleDTO, CancellationToken cancellationToken = default);

	Task<Result<RolesPage>> GetAsync(PagingOptions? pagingOptions = null, CancellationToken cancellationToken = default);
}