using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TestTask.Application.Contracts;
using TestTask.Application.Implementations.Services;
using TestTask.Application.Implementations.Validators;
using TestTask.Application.Services;

namespace TestTask.Application.Implementations;

public static class Registrator
{
	public static IServiceCollection AddApplicationLayer(this IServiceCollection services) => services
		.AddScoped<IUserService, UserService>()
		.AddTransient<IFilteringOptionsChecker<UsersFilteringOptions>, UsersFilteringOptionsChecker>()
		.AddScoped<IRoleService, RoleService>()
		.AddValidatorsFromAssembly(typeof(UserSetRoleDTOValidator).Assembly)
		.AddScoped(typeof(IValidator<>), typeof(ValueIdValidator<>))
		;
}
