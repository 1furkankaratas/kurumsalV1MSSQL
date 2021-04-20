using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using UI.Models;

namespace UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISliderService _sliderService;
        private readonly IPageService _pageService;
        private readonly ISettingService _settingService;

        public HomeController(ISliderService sliderService, IPageService pageService, ISettingService settingService)
        {
            _sliderService = sliderService;
            _pageService = pageService;
            _settingService = settingService;
        }

        [Route("Anasayfa")]
        [Route("")]
        public IActionResult Index()
        {
            var result = _sliderService.GetAll();
            if (result.Success)
            {
                var result2 = _pageService.GetByType(1);
                if (result2.Success)
                {
                    HomePageDto homePageDto = new HomePageDto
                    {
                        Sliders = result.Data,
                        Pages = result2.Data
                    };

                    return View(homePageDto);
                }
                
            }
            return View();
        }

        [Route("İletisim")]
        public IActionResult Contact()
        {

            var result = _settingService.GetAll();
            if (result.Success)
            {
                ContactPageDto contactPageDto = new ContactPageDto
                {
                    Address = result.Data.Address,
                    Email = result.Data.Email,
                    Maps = result.Data.Maps,
                    Phone = result.Data.Phone,
                    WorkTime = result.Data.WorkTime
                };

                return View(contactPageDto);

            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
