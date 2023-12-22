using FluentValidation;
using TestTask.WebApi.ViewModels;

namespace TestTask.WebApi.Validators;

public class PagingOptionsModelValidator : AbstractValidator<PagingOptionsModel>
{
    public PagingOptionsModelValidator()
    {
        RuleFor(e => e.PageIndex).GreaterThanOrEqualTo(1);
        RuleFor(e => e.PageSize).GreaterThanOrEqualTo(1);
    }
}
