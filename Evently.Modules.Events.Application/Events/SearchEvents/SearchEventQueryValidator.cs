using FluentValidation;

namespace Evently.Modules.Events.Application.Events.SearchEvents
{
    internal sealed class SearchEventQueryValidator : AbstractValidator<SearchEventQuery>
    {
        public SearchEventQueryValidator()
        {
            RuleFor(x => x.Search.Page).GreaterThanOrEqualTo(0).WithMessage(EventValidatorErrors.PageInvalid);
            RuleFor(x => x.Search.PageSize).GreaterThanOrEqualTo(0).WithMessage(EventValidatorErrors.PageSizeInvalid);
        }
    }
}
