using TestTask.Domain.Common;

namespace TestTask.Domain.Entities;

public class User : Entity<UserId>
{
	public required int Age { get; set; }

	public required string Email { get; init; }

	public required string FullName { get; set; }

	public required string PasswordHash { get; init; }

	public ICollection<UserRole> Roles { get; init; } = new List<UserRole>();
}

public record UserId(Guid Value) : IValueId<UserId>
{
	public static UserId Create() => new(Guid.NewGuid());
}
