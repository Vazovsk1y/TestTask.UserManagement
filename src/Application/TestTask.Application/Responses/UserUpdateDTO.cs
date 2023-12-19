using TestTask.Domain.Entities;

namespace TestTask.Application.Responses;

public record UserUpdateDTO(UserId Id, string FullName, int Age);
