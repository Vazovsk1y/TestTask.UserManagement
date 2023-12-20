using TestTask.Domain.Common;

namespace TestTask.Domain.Entities;

public class User : Entity<UserId>
{
	public required int Age { get; set; }

	public required string Email { get; set; }

	public required string FullName { get; set; }

	public required string PasswordHash { get; set; }

	public ICollection<UserRole> Roles { get; set; } = new List<UserRole>();

	public User() : base() { }
}

public record UserId(Guid Value) : IValueId<UserId>
{
	public static UserId Create() => new(Guid.NewGuid());
}
