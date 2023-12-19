using TestTask.Domain.Entities;

namespace TestTask.Application.Responses;

public record UserDTO(UserId Id, string FullName, string Email, int Age, IReadOnlyCollection<RoleDTO> Roles);
