using FluentValidation;
using TestTask.Application.Contracts;
using TestTask.Domain.Entities;

namespace TestTask.Application.Implementations.Validators;

public class UserAddToRoleDTOValidator : AbstractValidator<UserAddToRoleDTO>
{
	public UserAddToRoleDTOValidator(IValidator<UserId> userIdValidator, IValidator<RoleId> roleIdValidator)
	{
		RuleFor(e => e.ToId).NotNull().SetValidator(userIdValidator);
		RuleFor(e => e.RoleId).NotNull().SetValidator(roleIdValidator);
	}
}
