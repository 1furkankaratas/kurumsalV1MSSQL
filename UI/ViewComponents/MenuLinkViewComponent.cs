using System.Collections.Generic;
using System.Linq;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace UI.ViewComponents
{
    public class MenuLinkViewComponent : ViewComponent
    {
        private readonly IPageService _pageService;

        public MenuLinkViewComponent(IPageService pageService)
        {
            _pageService = pageService;
        }

        public IViewComponentResult Invoke()
        {
            var result = _pageService.GetByType(0);

            if (result.Success)
            {
                return View(result.Data);
            }

            return View();
        }
    }
}