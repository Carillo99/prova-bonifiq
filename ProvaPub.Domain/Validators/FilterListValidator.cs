using FluentValidation;
using ProvaPub.Domain.DTO.Report;

namespace ProvaPub.Domain.Validators
{
    public class FilterListValidator : AbstractValidator<FilterDTO>
    {
        public FilterListValidator()
        {
            RuleFor(a => a.Page).Must(x => x >= 0).WithMessage("Invalid page number.");
            RuleFor(e => e.Rows).Must(x => x >= 0).WithMessage("Invalid number of lines.");
        }
    }
}
