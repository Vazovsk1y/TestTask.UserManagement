using TestTask.Application.Contracts;
using TestTask.Domain.Entities;

namespace TestTask.Application.Implementations.Extensions;

internal static class Mapper
{
    public static UserDTO ToDTO(this User user)
    {
        return new UserDTO(
            user.Id.Value,
            user.FullName,
            user.Email,
            user.Age,
            user.Roles.Select(r => new RoleDTO(r.RoleId.Value, r.Role!.Title)).ToList());
    }

    public static RoleDTO ToDTO(this Role role)
    {
        return new RoleDTO(role.Id.Value, role.Title);
    }
}
