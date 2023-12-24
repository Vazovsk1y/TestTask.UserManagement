using TestTask.Application.Contracts;
using TestTask.Application.Contracts.Common;

namespace TestTask.Application.Services;

public interface IRoleService
{
	Task<Result> SetRoleAsync(UserSetRoleDTO userSetRoleDTO, CancellationToken cancellationToken = default);

	Task<Result<IReadOnlyCollection<RoleDTO>>> GetAllAsync(CancellationToken cancellationToken = default);
}