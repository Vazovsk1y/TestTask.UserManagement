using TestTask.Domain.Entities;

namespace TestTask.Application.Contracts;

public record UserAddToRoleDTO(UserId ToId, RoleId RoleId);