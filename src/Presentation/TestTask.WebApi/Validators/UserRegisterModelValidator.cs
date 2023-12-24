using FluentValidation;
using TestTask.WebApi.ViewModels;

namespace TestTask.WebApi.Validators;

public class UserRegisterModelValidator : AbstractValidator<UserRegisterModel>
{
	public UserRegisterModelValidator() 
	{
		RuleFor(e => e.Age).GreaterThanOrEqualTo(0);
		RuleFor(e => e.FullName).NotEmpty();
		RuleFor(e => e.Email).NotEmpty().EmailAddress();
		RuleFor(e => e.Password).NotEmpty();
	}
}