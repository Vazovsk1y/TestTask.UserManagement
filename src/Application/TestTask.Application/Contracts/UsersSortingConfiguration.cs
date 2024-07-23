using MikesPaging.AspNetCore.Common;
using TestTask.Domain.Entities;

namespace TestTask.Application.Contracts;

public class UsersSortingConfiguration : SortingConfiguration<User, UsersSortingEnum>
{
    public UsersSortingConfiguration()
    {
        SortFor(UsersSortingEnum.ByRolesCount, e => e.Roles.Count);
    }
}