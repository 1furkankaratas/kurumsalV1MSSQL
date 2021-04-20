using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class SocialValidator : AbstractValidator<SocialMedia>
    {
        public SocialValidator()
        {
            RuleFor(p => p.Type).NotEmpty();
            RuleFor(p => p.Link).NotEmpty();
            RuleFor(p => p.Link).MinimumLength(5);
            RuleFor(p => p.Link).Must(StartWithHttp).WithMessage("Link ismi 'http' ile başlamalı"); ;
        }

        private bool StartWithHttp(string arg)
        {
            return arg.StartsWith("http");
        }
    }
}