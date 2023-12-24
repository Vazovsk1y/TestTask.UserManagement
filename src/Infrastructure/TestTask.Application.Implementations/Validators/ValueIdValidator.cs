using FluentValidation;

namespace TestTask.Application.Implementations.Validators;

public class ValueIdValidator<T> : AbstractValidator<T> where T : IValueId<T>
{
	public ValueIdValidator()
	{
		RuleFor(e => e.Value).NotEqual(e => Guid.Empty);
	}
}