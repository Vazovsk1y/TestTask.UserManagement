using TestTask.Application.Contracts.Common;

namespace TestTask.Application.Contracts;

public record RolesPage : Page<RoleDTO>
{
	public RolesPage(IReadOnlyCollection<RoleDTO> roles, int totalRolesCount, PagingOptions? pagingOptions = null) : base(roles, totalRolesCount, pagingOptions)
	{
	}
}