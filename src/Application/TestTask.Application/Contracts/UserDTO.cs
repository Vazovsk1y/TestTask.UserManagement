namespace TestTask.Application.Contracts;

public record UserDTO(Guid Id, string FullName, string Email, int Age, IReadOnlyCollection<RoleDTO> Roles);
