using TestTask.Domain.Entities;

namespace TestTask.Application.Responses;

public record UserAddRoleDTO(UserId FromId, UserId ToId, RoleId RoleId);