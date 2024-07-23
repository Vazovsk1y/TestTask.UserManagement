namespace TestTask.Application.Contracts;

public record UserAddToRoleDTO(Guid TargetUserId, Guid RoleId);