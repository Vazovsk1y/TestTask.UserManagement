using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using MikesPaging.AspNetCore;

namespace TestTask.Application.Implementations.Extensions;

public static class Registrator
{
	public static IServiceCollection AddApplicationLayer(this IServiceCollection services) => services
		.AddScoped<IUserService, UserService>()
		.AddScoped<IRoleService, RoleService>()
		.AddScoped<IAuthenticationService, AuthenticationService>()
		.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
		.AddPaging()
		.AddFiltering()
		.AddSorting()
		.AddSortingConfigurationsFromAssembly(Assembly.GetExecutingAssembly())
		.AddFilteringConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
}
