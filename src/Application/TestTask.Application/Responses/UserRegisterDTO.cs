namespace TestTask.Application.Responses;

public record UserRegisterDTO(string FullName, int Age, UserCredentialsDTO Credentials);
