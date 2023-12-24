using FluentValidation;
using TestTask.Application.Contracts.Common;

namespace TestTask.Application.Implementations.Validators;

public class PagingOptionsValidator : AbstractValidator<PagingOptions>
{
	public PagingOptionsValidator()
	{
		RuleFor(e => e.PageIndex).GreaterThanOrEqualTo(1);
		RuleFor(e => e.PageSize).GreaterThanOrEqualTo(1);
	}
}