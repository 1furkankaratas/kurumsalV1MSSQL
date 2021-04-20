using System.Collections.Generic;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace UI.ViewComponents
{
    public class LogoViewComponent:ViewComponent
    {
        private readonly ISettingService _settingService;

        public LogoViewComponent(ISettingService settingService)
        {
            _settingService = settingService;
        }


        public IViewComponentResult Invoke()
        {
            var result = _settingService.GetAll();

            
            if (result.Success)
            {
                return View(result.Data);
            }

            return View();
        }
    }

    public class AdminLogoViewComponent : ViewComponent
    {
        private readonly ISettingService _settingService;

        public AdminLogoViewComponent(ISettingService settingService)
        {
            _settingService = settingService;
        }


        public IViewComponentResult Invoke()
        {
            var result = _settingService.GetAll();

            if (result.Success)
            {
                return View(result.Data);
            }

            return View();
        }
    }
}