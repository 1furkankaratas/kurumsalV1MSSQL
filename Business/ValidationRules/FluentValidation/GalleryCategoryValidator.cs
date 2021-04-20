using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class GalleryCategoryValidator : AbstractValidator<GalleryCategory>
    {
        public GalleryCategoryValidator()
        {
            RuleFor(p => p.GalleryImageId).NotEmpty();
            RuleFor(p => p.CategoryImageId).NotEmpty();
        }

    }
}