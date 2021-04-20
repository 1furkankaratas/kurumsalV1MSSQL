using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class GalleryImageValidator : AbstractValidator<GalleryImage>
    {
        public GalleryImageValidator()
        {
            RuleFor(p => p.Source).NotEmpty();
            RuleFor(p => p.Description).NotEmpty();
            RuleFor(p => p.Description).MinimumLength(5);
        }

    }
}