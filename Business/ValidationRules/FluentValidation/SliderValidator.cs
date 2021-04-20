using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class SliderValidator : AbstractValidator<Slider>
    {
        public SliderValidator()
        {
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.Source).NotEmpty();
            RuleFor(p => p.Name).MinimumLength(3);
        }

    }
}