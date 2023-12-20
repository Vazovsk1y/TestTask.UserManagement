using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection;
using System.Text.Json;
using TestTask.Domain.Common;
using TestTask.Domain.Constants;
using TestTask.Domain.Entities;

namespace TestTask.DAL;

internal static class Extensions
{
	private const string UsersSeedResourceName = "TestTask.DAL.usersSeed.json";

	public static void ConfigureId<TEntity, TId>(this EntityTypeBuilder<TEntity> typeBuilder)
		where TEntity : Entity<TId>
		where TId : IValueId<TId>
	{
		typeBuilder.HasKey(e => e.Id);

		typeBuilder.Property(e => e.Id)
				.HasConversion(
					e => e.Value,
					e => (TId)Activator.CreateInstance(typeof(TId), e)!
				);
	}

	public static void SeedData(this ModelBuilder modelBuilder)
	{
		var roles = new string[] { Roles.Admin, Roles.User, Roles.SuperAdmin, Roles.Support }.Select(e => new Role { Title = e });

		using var resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(UsersSeedResourceName) 
			?? throw new InvalidOperationException("Users seed resourse file not found.");

		var usersFromSeedFile = JsonSerializer.Deserialize<IEnumerable<UserSeed>>(resourceStream) ?? Enumerable.Empty<UserSeed>();
		var users = usersFromSeedFile.Select(e => new User
		{
			Age = e.Age,
			Email = e.Email,
			FullName = e.FullName,
			PasswordHash = BCrypt.Net.BCrypt.HashPassword(e.Password),
		});


		var usersRoles = new List<UserRole>();
        foreach (var userSeed in usersFromSeedFile)
        {
			var user = users.First(e => e.Email == userSeed.Email);
			usersRoles.AddRange(roles.Where(r => userSeed.Roles.Contains(r.Title)).Select(e => new UserRole { RoleId = e.Id, UserId = user.Id }));
        }

		modelBuilder.Entity<Role>().HasData(roles);
		modelBuilder.Entity<User>().HasData(users);
		modelBuilder.Entity<UserRole>().HasData(usersRoles);
    }
}

public class UserSeed
{
	public required string FullName { get; init; }

	public required string Email { get; init; }

	public required string Password { get; init; }

	public required int Age { get; init; }

	public required IEnumerable<string> Roles { get; init; }
}
