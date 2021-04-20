using System.Collections.Generic;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace UI.ViewComponents
{
    public class SocialViewComponent:ViewComponent
    {
        private readonly ISocialService _socialService;

        public SocialViewComponent(ISocialService socialService)
        {
            _socialService = socialService;
        }

        public IViewComponentResult Invoke()
        {
            var result = _socialService.GetAll();

            if (result.Success)
            {
                return View(result.Data);
            }

            return View();
        }
    }
}