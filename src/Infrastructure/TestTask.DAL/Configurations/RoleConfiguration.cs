using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestTask.Domain.Entities;

namespace TestTask.DAL.Configurations;

internal class RoleConfiguration : IEntityTypeConfiguration<Role>
{
	public void Configure(EntityTypeBuilder<Role> builder)
	{
		builder.ConfigureId<Role, RoleId>();

		builder.HasIndex(e => e.Title).IsUnique();

		builder
			.HasMany<UserRole>()
			.WithOne(e => e.Role)
			.HasForeignKey(e => e.RoleId);
	}
}
