using System.Collections.Generic;
using System.Linq;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace UI.ViewComponents
{
    public class GalleryPageCategoryViewComponent : ViewComponent
    {
        private readonly ICategoryImageService _categoryImageService;

        public GalleryPageCategoryViewComponent(ICategoryImageService categoryImageService)
        {
            _categoryImageService = categoryImageService;
        }


        public IViewComponentResult Invoke()
        {
            var result = _categoryImageService.GetAll();

            if (result.Success)
            {
                var data = result.Data.Where(x => x.IsActive).ToList();
                return View(data);
            }

            return View();
        }
    }
}