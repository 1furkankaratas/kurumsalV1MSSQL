using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class PageValidator : AbstractValidator<Page>
    {
        public PageValidator()
        {
            RuleFor(p => p.Title).NotEmpty();
            RuleFor(p => p.Image).NotEmpty();
            RuleFor(p => p.Text).NotEmpty();
            RuleFor(p => p.Type).NotEmpty();
            RuleFor(p => p.Description).NotEmpty();
            RuleFor(p => p.Title).MinimumLength(5);
            RuleFor(p => p.Text).MinimumLength(5);
            RuleFor(p => p.Description).MinimumLength(5);
        }

    }
}