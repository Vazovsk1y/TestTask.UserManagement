using TestTask.Domain.Entities;

namespace TestTask.Application.Contracts;

public record UserUpdateDTO(UserId Id, string FullName, int Age);
