using Microsoft.EntityFrameworkCore;
using TestTask.Domain.Entities;

namespace TestTask.DAL;

public class TestTaskDbContext(DbContextOptions<TestTaskDbContext> dbContextOptions) : DbContext(dbContextOptions)
{
	public DbSet<User> Users { get; set; }

	public DbSet<Role> Roles { get; set; }

	public DbSet<UserRole> UsersRoles { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(TestTaskDbContext).Assembly);
		modelBuilder.SeedData();
	}
}
