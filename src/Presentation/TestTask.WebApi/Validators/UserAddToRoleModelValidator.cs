using FluentValidation;
using TestTask.WebApi.ViewModels;

namespace TestTask.WebApi.Validators;

public class UserAddToRoleModelValidator : AbstractValidator<UserAddToRoleModel>
{
	public UserAddToRoleModelValidator()
	{
		RuleFor(e => e.UserId).NotEmpty().NotEqual(Guid.Empty);
		RuleFor(e => e.RoleId).NotEmpty().NotEqual(Guid.Empty);
	}
}