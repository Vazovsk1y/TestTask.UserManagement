using FluentValidation;
using TestTask.Application.Responses;

namespace TestTask.Application.Implementations.Validators;

public class UserCredentialsDTOValidator : AbstractValidator<UserCredentialsDTO>
{
	public UserCredentialsDTOValidator()
	{
		RuleFor(e => e.Email).EmailAddress();
		RuleFor(e => e.Password).NotEmpty().WithMessage($"{nameof(UserCredentialsDTO.Password)} property is required.");
	}
}
