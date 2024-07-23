using FluentValidation;
using TestTask.Application.Contracts;

namespace TestTask.Application.Implementations.Validators;

public class UserRegisterDTOValidator : AbstractValidator<UserRegisterDTO>
{
	public UserRegisterDTOValidator(IValidator<UserCredentialsDTO> userCredentialsDTOValidator)
	{
		RuleFor(e => e.Age).GreaterThanOrEqualTo(0).WithMessage("Age must be positive number.");
		RuleFor(e => e.FullName).NotEmpty().WithMessage("Full name property is required.");
		RuleFor(e => e.Credentials).SetValidator(userCredentialsDTOValidator);
	}
}
