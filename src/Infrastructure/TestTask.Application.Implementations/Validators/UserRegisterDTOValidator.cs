using FluentValidation;
using TestTask.Application.Responses;

namespace TestTask.Application.Implementations.Validators;

public class UserRegisterDTOValidator : AbstractValidator<UserRegisterDTO>
{
	public UserRegisterDTOValidator(IValidator<UserCredentialsDTO> userCredentialsDTOValidator)
	{
		RuleFor(e => e.Age).GreaterThanOrEqualTo(0).WithMessage("Age must be positive number.");
		RuleFor(e => e.FullName).NotEmpty().WithMessage($"{nameof(UserRegisterDTO.FullName)} property is required.");
		RuleFor(e => e.Credentials).SetValidator(userCredentialsDTOValidator);
	}
}
