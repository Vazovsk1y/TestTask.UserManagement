﻿using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TestTask.DAL;
using TestTask.Domain.Entities;

namespace TestTask.WebApi;

public static class Extensions
{
	public static void MigrateDatabase(this WebApplication app)
	{
		using var scope = app.Services.CreateScope();
		var dbContext = scope.ServiceProvider.GetRequiredService<TestTaskDbContext>();
		dbContext.Database.Migrate();
	}

	public static UserId GetUserId(this HttpContext httpContext)
	{
		var id = Guid.Parse(httpContext.User.Claims.Single(e => e.Type == ClaimTypes.NameIdentifier).Value);
		return new UserId(id);
	}
}
