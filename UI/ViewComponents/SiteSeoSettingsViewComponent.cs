using System.Collections.Generic;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace UI.ViewComponents
{
    public class SiteSeoSettingsViewComponent : ViewComponent
    {
        private readonly ISettingService _settingService;

        public SiteSeoSettingsViewComponent(ISettingService settingService)
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