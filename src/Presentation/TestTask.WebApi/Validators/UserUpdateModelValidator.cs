using FluentValidation;
using TestTask.WebApi.ViewModels;

namespace TestTask.WebApi.Validators;

public class UserUpdateModelValidator : AbstractValidator<UserUpdateModel>
{
	public UserUpdateModelValidator()
	{
		RuleFor(e => e.FullName).NotEmpty();
		RuleFor(e => e.Age).GreaterThanOrEqualTo(0);
		RuleFor(e => e.UserId).NotEmpty().NotEqual(Guid.Empty);
	}
}