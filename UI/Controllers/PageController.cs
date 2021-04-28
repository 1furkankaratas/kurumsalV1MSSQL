using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;

namespace UI.Controllers
{
    public class PageController : Controller
    {
        private readonly IPageService _pageService;

        public PageController(IPageService pageService)
        {
            _pageService = pageService;
        }

        [Route("{title}-{id:int}")]
        public IActionResult Index(int id)
        {

            var result = _pageService.GetById(id);

            if (result.Success)
            {
                if (result.Data.IsActive)
                {
                    return View(result.Data);
                }
            }

            return RedirectToAction("Index","Home");
        }
    }
}
