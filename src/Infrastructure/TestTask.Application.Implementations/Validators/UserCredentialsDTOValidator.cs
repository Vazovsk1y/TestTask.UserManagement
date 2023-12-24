using FluentValidation;
using TestTask.Application.Contracts;

namespace TestTask.Application.Implementations.Validators;

public class UserCredentialsDTOValidator : AbstractValidator<UserCredentialsDTO>
{
	public UserCredentialsDTOValidator()
	{
		RuleFor(e => e.Email).NotEmpty().EmailAddress();
		RuleFor(e => e.Password).NotEmpty().WithMessage($"Password property is required.");
	}
}
