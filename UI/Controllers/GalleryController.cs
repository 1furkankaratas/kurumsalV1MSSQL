using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using DataAccess.Abstract;

namespace UI.Controllers
{
    [Route("Galeri")]
    public class GalleryController : Controller
    {
        private readonly IGalleryService _galleryService;

        public GalleryController(IGalleryService galleryService)
        {
            _galleryService = galleryService;
        }

        [Route("")]
        public IActionResult Index()
        {

            var result = _galleryService.GetGalleries();
            if (result.Success)
            {
                return View(result.Data.Where(x => x.IsActive == true).ToList());
            }
            return View();
        }
    }
}
