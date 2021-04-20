using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class SettingValidator : AbstractValidator<Setting>
    {
        public SettingValidator()
        {
            RuleFor(p => p.Title).NotEmpty();
            RuleFor(p => p.Description).NotEmpty();
            RuleFor(p => p.Address).NotEmpty();
            RuleFor(p => p.CompanyName).NotEmpty();
            RuleFor(p => p.Email).NotEmpty();
            RuleFor(p => p.Maps).NotEmpty();
            RuleFor(p => p.Meta).NotEmpty();
            RuleFor(p => p.Phone).NotEmpty();
            RuleFor(p => p.WorkTime).NotEmpty();
            RuleFor(p => p.Title).MinimumLength(5);
            RuleFor(p => p.Description).MinimumLength(5);
            RuleFor(p => p.Address).MinimumLength(5);
            RuleFor(p => p.CompanyName).MinimumLength(5);
            RuleFor(p => p.Email).MinimumLength(5);
            RuleFor(p => p.Email).EmailAddress();
            RuleFor(p => p.Maps).MinimumLength(5);
            RuleFor(p => p.Meta).MinimumLength(5);
            RuleFor(p => p.Phone).MinimumLength(5);
            RuleFor(p => p.WorkTime).MinimumLength(5);

        }

    }
}