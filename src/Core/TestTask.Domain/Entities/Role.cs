using TestTask.Domain.Common;

namespace TestTask.Domain.Entities;

public class Role : Entity<RoleId>
{
	public required string Title { get; set; }

	public Role() : base() { }
}

public record RoleId(Guid Value) : IValueId<RoleId>
{
	public static RoleId Create() => new(Guid.NewGuid());
}
