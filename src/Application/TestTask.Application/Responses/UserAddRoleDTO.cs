using TestTask.Domain.Entities;

namespace TestTask.Application.Responses;

public record UserAddRoleDTO(UserId From, UserId To, RoleId RoleId);