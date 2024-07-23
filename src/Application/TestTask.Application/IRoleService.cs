using TestTask.Application.Contracts;
using TestTask.Domain.Entities;
using TestTask.Domain.Shared;

namespace TestTask.Application;

public interface IRoleService
{
	Task<Result> AddToRoleAsync(UserId requesterId, UserAddToRoleDTO dto, CancellationToken cancellationToken = default);

	Task<Result<IReadOnlyCollection<RoleDTO>>> GetAllAsync(CancellationToken cancellationToken = default);
}