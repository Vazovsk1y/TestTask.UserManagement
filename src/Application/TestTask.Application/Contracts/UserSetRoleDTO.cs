using TestTask.Domain.Entities;

namespace TestTask.Application.Contracts;

public record UserSetRoleDTO(UserId FromId, UserId ToId, RoleId RoleId);