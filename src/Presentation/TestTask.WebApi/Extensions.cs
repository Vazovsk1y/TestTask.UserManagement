using Microsoft.EntityFrameworkCore;
using TestTask.DAL;

namespace TestTask.WebApi;

public static class Extensions
{
	public static async Task MigrateDatabaseAsync(this WebApplication app)
	{
		using var scope = app.Services.CreateScope();
		var dbContext = scope.ServiceProvider.GetRequiredService<TestTaskDbContext>();
		await dbContext.Database.MigrateAsync();
	}
}
