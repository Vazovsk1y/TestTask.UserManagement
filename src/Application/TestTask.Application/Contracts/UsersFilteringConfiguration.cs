using MikesPaging.AspNetCore.Common;
using MikesPaging.AspNetCore.Common.Enums;
using TestTask.Domain.Entities;

namespace TestTask.Application.Contracts;

public class UsersFilteringConfiguration : FilteringConfiguration<User, UsersFilteringEnum>
{
    private UsersFilteringConfiguration()
    {
        FilterFor(UsersFilteringEnum.ByRoles, FilteringOperators.Contains, s =>
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(s);
            var roleId = Guid.Parse(s);
            return e => e.Roles.Any(r => r.RoleId == new RoleId(roleId));
        });
    }
}