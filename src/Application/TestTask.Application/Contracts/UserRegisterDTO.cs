namespace TestTask.Application.Contracts;

public record UserRegisterDTO(string FullName, int Age, UserCredentialsDTO Credentials);
