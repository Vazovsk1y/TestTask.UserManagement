namespace TestTask.Application.Implementations.Infrastructure;

public record JwtOptions
{
    public required string Audience { get; init; }
    public required string Issuer { get; init; }
    public required string SecretKey { get; init; }
    public required int LifetimeHoursCount { get; init; }
}