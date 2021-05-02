using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Constants;
using Entities.Concrete;
using Entities.Concrete.MicrosoftIdentity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using UI.Models;

namespace UI.Controllers
{
    [Authorize]
    [Route("panel")]
    public class ManagementPanelController : Controller
    {
        private readonly IPageService _pageService;
        private readonly ISettingService _settingService;
        private readonly ISocialService _socialService;
        private readonly ISliderService _sliderService;
        private readonly IGalleryService _galleryService;
        private readonly ICategoryImageService _categoryImageService;
        private readonly IGalleryCategoryService _galleryCategoryService;


        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        private AppUser CurrentUser => _userManager.FindByNameAsync(User.Identity.Name).Result;


        public ManagementPanelController(IPageService pageService, ISettingService settingService, ISocialService socialService, ISliderService sliderService, IGalleryService galleryService, ICategoryImageService categoryImageService, IGalleryCategoryService galleryCategoryService, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _pageService = pageService;
            _settingService = settingService;
            _socialService = socialService;
            _sliderService = sliderService;
            _galleryService = galleryService;
            _categoryImageService = categoryImageService;
            _galleryCategoryService = galleryCategoryService;
            _userManager = userManager;
            _signInManager = signInManager;
        }



        [Route("kayit")]
        [AllowAnonymous]
        public IActionResult Register()
        {
            var a = _userManager.Users.ToList();
            if (a.Count(x=>x.EmailConfirmed == true) == 0)
            {
                return View();
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [Route("kayit")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["GeneralError"] = Messages.RequiredInput;
                return View(model);
            }
            AppUser user = new AppUser
            {
                UserName = model.Username,
                Email = model.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                ViewData["GeneralSuccess"] = Messages.UserRegistered;
                return RedirectToAction("Login", "Auth");
            }

            ViewData["GeneralError"] = Messages.GeneralError;
            return RedirectToAction("Index", "Home");
        }


        [Route("")]
        [Route("anasayfa")]
        public IActionResult Index()
        {
            AdminIndexViewModel model = new AdminIndexViewModel
            {
                CategoryCount = _categoryImageService.GetAll().Data.Count,
                GalleryCount = _galleryService.GetAll().Data.Count,
                PageCount = _pageService.GetAll().Data.Count,
                SlideCount = _sliderService.GetAll().Data.Count,
                SocialCount = _socialService.GetAll().Data.Count
            };
            
            return View(model);
        }


        //slider Image

        [HttpGet]
        [Route("slider/liste")]
        public IActionResult ListSlider()
        {
            ViewData["GeneralSuccess"] = ViewData["GeneralSuccess"];
            var result = _sliderService.GetAll();

            if (result.Success)
            {
                return View(result.Data);
            }
            ViewData["GeneralError"] = Messages.GeneralError;
            return RedirectToAction("Index", "ManagementPanel");
        }

        [HttpGet]
        [Route("slider/ekle")]
        public IActionResult AddSlider()
        {

            return View();
        }

        [HttpPost]
        [Route("slider/ekle")]
        public IActionResult AddSlider(AddSliderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["GeneralError"] = Messages.RequiredInput;
                return View(model);
            }

            var source = _galleryService.UploadGalleryImage(model.File, ImageSavePaths.SliderSavePath);

            if (source.Success)
            {

                Slider slider = new Slider { Name = model.Name,Link = model.Link,LinkName = model.LinkName,Description = model.Description,Source = source.Data };


                var result = _sliderService.Add(slider);

                if (result.Success)
                {
                    ViewData["GeneralSuccess"] = result.Message;
                    return RedirectToAction("ListSlider", "ManagementPanel");
                }
            }

            ViewData["GeneralError"] = source.Message;
            return View(model);
        }


        [HttpGet]
        [Route("slider/sil")]
        public IActionResult DeleteSlider(int id)
        {
            if (id == 0)
            {
                ViewData["GeneralError"] = Messages.GeneralError;
                return RedirectToAction("ListSlider", "ManagementPanel");
            }

            var data = _sliderService.GetById(id);

            if (data.Data!=null)
            {
                var result = _sliderService.Delete(data.Data);

                if (result.Success)
                {
                    ViewData["GeneralSuccess"] = result.Message;
                    return RedirectToAction("ListSlider", "ManagementPanel");
                }
            }

            ViewData["GeneralError"] = Messages.GeneralError;

            return RedirectToAction("ListSlider", "ManagementPanel");
        }


        //Page

        [HttpGet]
        [Route("sayfa/liste")]
        public IActionResult ListPage()
        {
            var result = _pageService.GetAll();

            if (result.Success)
            {
                return View(result.Data);
            }

            ViewData["GeneralError"] = Messages.GeneralError;
            return RedirectToAction("Index", "ManagementPanel");
        }

        [HttpGet]
        [Route("sayfa/ekle")]
        public IActionResult AddPage()
        {

            return View();
        }

        [HttpPost]
        [Route("sayfa/ekle")]
        public IActionResult AddPage(AddPageViewModel model)
        {

            if (!ModelState.IsValid)
            {
                ViewData["GeneralError"] = Messages.RequiredInput;
                return View(model);
            }

            var source = _galleryService.UploadGalleryImage(model.File, ImageSavePaths.PageSavePath);

            if (source.Success)
            {
                Page page = new Page
                {
                    Title = model.Title,
                    Description = model.Description,
                    Image = source.Data,
                    Type = model.Type,
                    Text = model.Text
                };

                var result = _pageService.Add(page);

                if (result.Success)
                {
                    ViewData["GeneralSuccess"] = result.Message;
                    return RedirectToAction("ListPage", "ManagementPanel");
                }

            }

            ViewData["GeneralError"] = Messages.GeneralError;
            return View(model);
        }


        [Route("sayfa/sil")]
        [HttpGet]
        public IActionResult DeletePage(int id)
        {
            if (id == 0)
            {
                ViewData["GeneralError"] = Messages.GeneralError;
                return RedirectToAction("ListPage", "ManagementPanel");
            }

            var data = _pageService.GetById(id);
            if (data.Data!=null)
            {
                var result = _pageService.Delete(data.Data);

                if (result.Success)
                {
                    ViewData["GeneralSuccess"] = result.Message;
                    return RedirectToAction("ListPage", "ManagementPanel");
                }
            }

            ViewData["GeneralError"] = Messages.GeneralError;
            return RedirectToAction("ListPage", "ManagementPanel");
        }


        [HttpGet]
        [Route("sayfa/guncelle")]
        public IActionResult UpdatePage(int id)
        {
            if (id == 0)
            {
                ViewData["GeneralError"] = Messages.GeneralError;
                return RedirectToAction("ListPage", "ManagementPanel");
            }

            var data = _pageService.GetById(id);

            if (data.Data!=null)
            {
                return View(data.Data);
            }

            ViewData["GeneralError"] = Messages.GeneralError;

            return RedirectToAction("ListPage","ManagementPanel");

        }

        [Route("sayfa/guncelle")]
        [HttpPost]
        public IActionResult UpdatePage(UpdatePageViewModel model)
        {
            if (!ModelState.IsValid)
            {
                Page page = new Page
                {
                    Title = model.Title,
                    Description = model.Description,
                    Id = model.Id,
                    Text = model.Text,
                    Type = model.Type
                };
                ViewData["GeneralError"] = Messages.RequiredInput;
                return View(page);
            }
            var data = _pageService.GetById(model.Id);

            if (data.Data!=null)
            {
                string oldImage = data.Data.Image;

                data.Data.Title = model.Title;
                data.Data.Description = model.Description;
                data.Data.Text = model.Text;
                data.Data.Type = model.Type;

                if (model.File != null)
                {
                    var source = _galleryService.UploadGalleryImage(model.File,ImageSavePaths.PageSavePath);
                    if (source.Success)
                    {
                        data.Data.Image = source.Data;
                        var result = _pageService.UpdateWithImage(data.Data, oldImage);

                        if (result.Success)
                        {
                            ViewData["GeneralSuccess"] = result.Message;
                            return RedirectToAction("ListPage", "ManagementPanel");
                        }
                    }

                }
                else
                {
                    var result = _pageService.Update(data.Data);
                    if (result.Success)
                    {
                        ViewData["GeneralSuccess"] = result.Message;
                        return RedirectToAction("ListPage", "ManagementPanel");
                    }
                }
            }


            ViewData["GeneralError"] = Messages.GeneralError;
            return RedirectToAction("ListPage", "ManagementPanel");
        }


        //Gallery Category

        [HttpGet]
        [Route("kategori/galeri/liste")]
        public IActionResult ListGalleryCategory()
        {
            var result = _categoryImageService.GetAll();
            if (result.Success)
            {
                return View(result.Data);
            }
            ViewData["GeneralError"] = Messages.GeneralError;
            return RedirectToAction("Index", "ManagementPanel");
        }

        [Route("kategori/galeri/ekle")]
        [HttpGet]
        public IActionResult AddGalleryCategory()
        {
            return View();
        }

        [Route("kategori/galeri/ekle")]
        [HttpPost]
        public IActionResult AddGalleryCategory(CategoryImageViewModel model)
        {
            CategoryImage categoryImage = new CategoryImage
            {
                Name = model.Name
            };

            if (!ModelState.IsValid)
            {
                ViewData["GeneralError"] = Messages.RequiredInput;
                return View(categoryImage);
            }

            var result = _categoryImageService.Add(categoryImage);
            if (result.Success)
            {
                ViewData["GeneralSuccess"] = result.Message;
                return RedirectToAction("ListGalleryCategory", "ManagementPanel");
            }
            ViewData["GeneralError"] = Messages.GeneralError;
            return View(categoryImage);
        }

        [Route("kategori/galeri/sil")]
        [HttpGet]
        public IActionResult DeleteGalleryCategory(int id)
        {
            if (id == 0)
            {
                ViewData["GeneralError"] = Messages.GeneralError;
                return RedirectToAction("ListGalleryCategory", "ManagementPanel");
            }

            var data = _categoryImageService.GetById(id);
            if (data.Data!=null)
            {
                var result = _categoryImageService.Delete(data.Data);
                if (result.Success)
                {
                    var deleteCategory = _galleryCategoryService.GetCategoryId(data.Data.Id);

                    foreach (var delCat in deleteCategory.Data)
                    {
                        _galleryCategoryService.Delete(delCat);
                    }

                    ViewData["GeneralSuccess"] = result.Message;
                    return RedirectToAction("ListGalleryCategory", "ManagementPanel");
                }
            }

            ViewData["GeneralError"] = Messages.GeneralError;
            return RedirectToAction("ListGalleryCategory", "ManagementPanel");
        }

        [Route("kategori/galeri/guncelle")]
        [HttpGet]
        public IActionResult UpdateGalleryCategory(int id)
        {
            if (id == 0)
            {
                ViewData["GeneralError"] = Messages.GeneralError;
                return RedirectToAction("ListGalleryCategory", "ManagementPanel");
            }

            var data = _categoryImageService.GetById(id);

            if (data.Data!=null)
            {
                CategoryImageUploadViewModel model = new CategoryImageUploadViewModel
                {
                    Name = data.Data.Name,
                    Id = data.Data.Id
                };

                return View(model);

            }

            ViewData["GeneralError"] = Messages.GeneralError;
            return RedirectToAction("ListGalleryCategory", "ManagementPanel");

        }

        [Route("kategori/galeri/guncelle")]
        [HttpPost]
        public IActionResult UpdateGalleryCategory(CategoryImageUploadViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["GeneralError"] = Messages.RequiredInput;
                return View(model);
            }

            CategoryImage categoryImage = new CategoryImage
            {
                Name = model.Name,
                Id = model.Id
            };

            var result = _categoryImageService.Update(categoryImage);
            if (result.Success)
            {
                ViewData["GeneralSuccess"] = result.Message;
                return RedirectToAction("ListGalleryCategory", "ManagementPanel");
            }

            ViewData["GeneralError"] = Messages.GeneralError;
            return View(model);
        }

        //Gallery


        [Route("galeri/liste")]
        [HttpGet]
        public IActionResult ListGallery()
        {
            var result = _galleryService.GetGalleries();
            if (result.Success)
            {
                return View(result.Data);
            }
            ViewData["GeneralError"] = Messages.GeneralError;
            return View();
        }

        [Route("galeri/ekle")]
        [HttpGet]
        public IActionResult AddGallery()
        {
            ViewData["Categories"] = (List<CategoryImage>)(_categoryImageService.GetAll()).Data.Where(x=>x.IsActive).ToList();
            return View();
        }

        [Route("galeri/ekle")]
        [HttpPost]
        public IActionResult AddGallery(AddGalleryImageViewModel model)
        {

            if (!ModelState.IsValid)
            {
                ViewData["GeneralError"] = Messages.RequiredInput;
                ViewData["Categories"] = (List<CategoryImage>)(_categoryImageService.GetAll()).Data.Where(x => x.IsActive);
                return View(model);
            }

            var source = _galleryService.UploadGalleryImage(model.File, ImageSavePaths.GallerySavePath);
            if (source.Success)
            {
                GalleryImage galleryImage = new GalleryImage();
                galleryImage.Source = source.Data;
                galleryImage.Description = model.Description;

                var image = _galleryService.Add(galleryImage);

                if (image.Success)
                {
                    foreach (var categoryId in model.CategoriesId)
                    {
                        GalleryCategory galleryCategory = new GalleryCategory();

                        galleryCategory.GalleryImageId = galleryImage.Id;
                        galleryCategory.CategoryImageId = categoryId;

                        _galleryCategoryService.Add(galleryCategory);
                    }
                    ViewData["GeneralSuccess"] = image.Message;
                    return RedirectToAction("ListGallery", "ManagementPanel");
                }

                _galleryService.Delete(galleryImage);

            }

            _galleryService.DeleteGalleryImage(source.Data);
            ViewData["GeneralError"] = Messages.GeneralError;
            ViewData["Categories"] = (List<CategoryImage>)(_categoryImageService.GetAll()).Data;
            return View(model);
        }

        [Route("galeri/sil")]
        [HttpGet]
        public IActionResult DeleteGallery(int id)
        {
            if (id == 0)
            {
                ViewData["GeneralError"] = Messages.GeneralError;
                return RedirectToAction("ListGallery", "ManagementPanel");
            }

            var data = _galleryService.GetById(id);
            if (data.Data!=null)
            {
                var selectedCategories = _galleryCategoryService.GetGalleryId(data.Data.Id);
                var dataDelete = _galleryService.Delete(data.Data);
                if (dataDelete.Success)
                {
                    bool isSuccess = true;
                    foreach (var selCat in selectedCategories.Data)
                    {
                        var res = _galleryCategoryService.Delete(selCat);
                        isSuccess = res.Success;
                        if (!isSuccess)
                        {
                            break;
                        }
                    }

                    if (!isSuccess)
                    {
                        foreach (var selCat in selectedCategories.Data)
                        {
                            _galleryCategoryService.Add(selCat);
                        }
                    }
                    else
                    {
                        ViewData["GeneralSuccess"] = dataDelete.Message;
                        return RedirectToAction("ListGallery", "ManagementPanel");
                    }

                }
                _galleryService.Add(data.Data);
            }

            ViewData["GeneralError"] = Messages.GeneralError;
            return RedirectToAction("ListGallery", "ManagementPanel");
        }

        [Route("galeri/guncelle")]
        [HttpGet]
        public IActionResult UpdateGallery(int id)
        {

            if (id == 0)
            {
                ViewData["GeneralError"] = Messages.GeneralError;
                return RedirectToAction("ListGallery", "ManagementPanel");
            }

            var data = _galleryService.GetById(id);

            if (data.Data==null)
            {
                ViewData["GeneralError"] = Messages.GeneralError;
                return RedirectToAction("ListGallery", "ManagementPanel");
            }

            UpdateGalleryPageModel model = new UpdateGalleryPageModel
            {
                Description = data.Data.Description,
                Id = data.Data.Id
            };



            var selectBox = GetCategoryImageSelectBoxView(data.Data);





            ViewData["AllCategories"] = selectBox;
            

            return View(model);
        }

        private List<CategoryImageSelectBoxViewModel> GetCategoryImageSelectBoxView(GalleryImage data)
        {
            var categories = _galleryCategoryService.GetGalleryId(data.Id);

            List<CategoryImageSelectBoxViewModel> selectBoxes = new List<CategoryImageSelectBoxViewModel>();

            foreach (var i in (List<CategoryImage>)(_categoryImageService.GetAll()).Data)
            {
                CategoryImageSelectBoxViewModel selectBox = new CategoryImageSelectBoxViewModel();
                selectBox.Id = i.Id;
                selectBox.Name = i.Name;

                foreach (var cat in categories.Data)
                {
                    if (!selectBoxes.Contains(selectBox) || !selectBox.IsSelected)
                    {
                        if (cat.CategoryImageId == i.Id)
                        {
                            selectBox.IsSelected = true;
                            if (!selectBoxes.Contains(selectBox))
                            {
                                selectBoxes.Add(selectBox);
                            }
                        }

                        if (cat.CategoryImageId != i.Id)
                        {
                            selectBox.IsSelected = false;
                            if (!selectBoxes.Contains(selectBox))
                            {
                                selectBoxes.Add(selectBox);
                            }
                        }

                    }
                }
            }

            return selectBoxes;
        }

        [Route("galeri/guncelle")]
        [HttpPost]
        public IActionResult UpdateGallery(UpdateGalleryPageModel model)
        {
            var data = _galleryService.GetById(model.Id);
            if (!ModelState.IsValid)
            {
                ViewData["GeneralError"] = Messages.GeneralError;
                var selectBox = GetCategoryImageSelectBoxView(data.Data);

                ViewData["AllCategories"] = selectBox;
                return View(model);
            }

            string oldImage = data.Data.Source;

            data.Data.Description = model.Description;

            if (model.File != null)
            {
                var source = _galleryService.UploadGalleryImage(model.File, ImageSavePaths.GallerySavePath);
                if (source.Success)
                {
                    data.Data.Source = source.Data;
                    var result = _galleryService.Update(data.Data, oldImage);

                    if (result.Success)
                    {
                        bool isSuccess = true;
                        var oldCategories = _galleryCategoryService.GetGalleryId(data.Data.Id);

                        foreach (var oldCat in oldCategories.Data)
                        {
                            var res = _galleryCategoryService.Delete(oldCat);
                            isSuccess = res.Success;
                            if (!isSuccess)
                            {
                                break;
                            }

                        }

                        if (!isSuccess)
                        {
                            foreach (var oldCat in oldCategories.Data)
                            {
                                _galleryCategoryService.Add(oldCat);
                            }
                        }
                        else
                        {
                            
                            foreach (var newCat in model.CategoriesId)
                            {
                                GalleryCategory galleryCategory = new GalleryCategory();
                                galleryCategory.GalleryImageId = model.Id;
                                galleryCategory.CategoryImageId = newCat;
                                _galleryCategoryService.Add(galleryCategory);
                            }
                        }

                        ViewData["GeneralSuccess"] = result.Message;
                        return RedirectToAction("ListGallery", "ManagementPanel");
                    }
                }

            }
            else
            {
                var result = _galleryService.Update(data.Data, "");
                if (result.Success)
                {
                    bool isSuccess = true;
                    var oldCategories = _galleryCategoryService.GetGalleryId(data.Data.Id);

                    foreach (var oldCat in oldCategories.Data)
                    {
                        var res = _galleryCategoryService.Delete(oldCat);
                        isSuccess = res.Success;
                        if (!isSuccess)
                        {
                            break;
                        }

                    }

                    if (!isSuccess)
                    {
                        foreach (var oldCat in oldCategories.Data)
                        {
                            _galleryCategoryService.Add(oldCat);
                        }
                    }
                    else
                    {
                        
                        foreach (var newCat in model.CategoriesId)
                        {
                            GalleryCategory galleryCategory = new GalleryCategory();
                            galleryCategory.GalleryImageId = model.Id;
                            galleryCategory.CategoryImageId = newCat;
                            _galleryCategoryService.Add(galleryCategory);
                        }
                    }
                    ViewData["GeneralSuccess"] = result.Message;
                    return RedirectToAction("ListGallery", "ManagementPanel");
                }
            }


            ViewData["GeneralError"] = Messages.GeneralError;
            return RedirectToAction("ListGallery", "ManagementPanel");
        }


        //Settings

        [Route("ayarlar")]
        [HttpGet]
        public IActionResult UpdateSettings()
        {
            var data = _settingService.GetAll();
            if (data.Data!=null)
            {
                SettingViewModel model = new SettingViewModel
                {
                    Title = data.Data.Title,
                    Description = data.Data.Description,
                    Address = data.Data.Address,
                    CompanyName = data.Data.CompanyName,
                    Email = data.Data.Email,
                    Id = data.Data.Id,
                    Maps = data.Data.Maps,
                    Meta = data.Data.Meta,
                    Phone = data.Data.Phone,
                    WorkTime = data.Data.WorkTime,
                    WhatsAppText = data.Data.WhatsAppText,
                    WhatsAppPhone = data.Data.WhatsAppPhone
                };

                return View(model);
            }

            ViewData["GeneralError"] = Messages.GeneralError;
            return RedirectToAction("Index", "ManagementPanel");
        }

        [Route("ayarlar")]
        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 600000)]
        public IActionResult UpdateSettings(SettingViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["GeneralError"] = Messages.RequiredInput;
                return View(model);
            }

            string oldLogoSource = (_settingService.GetAll()).Data.Logo;
            string oldFaviconSource = (_settingService.GetAll()).Data.Favicon;
            Setting setting = new Setting
            {
                Title = model.Title,
                Description = model.Description,
                Address = model.Address,
                CompanyName = model.CompanyName,
                Email = model.Email,
                Id = model.Id,
                Maps = model.Maps,
                Meta = model.Meta,
                Phone = model.Phone,
                WorkTime = model.WorkTime,
                WhatsAppText = model.WhatsAppText,
                WhatsAppPhone = model.WhatsAppPhone,
                Favicon = oldFaviconSource,
                Logo = oldLogoSource
            };

            if (model.Logo != null)
            {
                var logoResult = _settingService.UploadLogo(model.Logo);
                if (logoResult.Success)
                {
                    setting.Logo = logoResult.Data;
                }
            }

            if (model.Favicon != null)
            {
                var faviconResult = _settingService.UploadFavicon(model.Favicon);
                if (faviconResult.Success)
                {
                    setting.Favicon = faviconResult.Data;
                }
            }

            var result = _settingService.Update(setting);

            if (result.Success)
            {
                ViewData["GeneralSuccess"] = result.Message;
                return RedirectToAction("Index", "ManagementPanel");
            }
            return View(model);
        }

        //Socials


        [Route("sosyal/liste")]
        [HttpGet]
        public IActionResult ListSocialAccounts()
        {
            var data = _socialService.GetAll();

            if (data.Data!=null)
            {
                return View(data.Data);
            }

            ViewData["GeneralError"] = Messages.GeneralError;
            return RedirectToAction("Index", "ManagementPanel");
        }

        [HttpGet]
        [Route("sosyal/sil")]
        public IActionResult DeleteSocialAccounts(int id)
        {
            if (id==0)
            {
                ViewData["GeneralError"] = Messages.GeneralError;
                return RedirectToAction("ListSocialAccounts", "ManagementPanel");
            }

            var data = _socialService.GetById(id);

            if (data.Data!=null)
            {
                var result = _socialService.Delete(data.Data);

                if (result.Success)
                {
                    ViewData["GeneralSuccess"] = result.Message;
                    return RedirectToAction("ListSocialAccounts", "ManagementPanel");
                }
            }

            ViewData["GeneralError"] = Messages.GeneralError;
            return RedirectToAction("ListSocialAccounts", "ManagementPanel");

        }

        [HttpGet]
        [Route("sosyal/ekle")]
        public IActionResult AddSocialAccounts()
        {
            return View();
        }

        [Route("sosyal/ekle")]
        [HttpPost]
        public IActionResult AddSocialAccounts(SocialMediaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["GeneralError"] = Messages.RequiredInput;
                return View(model);
            }

            SocialMedia socialMedia = new SocialMedia
            {
                Link = model.Link,
                Type = model.Type
            };

            var data = _socialService.Add(socialMedia);

            if (data.Success)
            {
                ViewData["GeneralSuccess"] = data.Message;
                return RedirectToAction("ListSocialAccounts", "ManagementPanel");
            }

            ViewData["GeneralError"] = Messages.GeneralError;
            return View(model);
        }

        [Route("kullanici/sifre")]
        public IActionResult PasswordChange()
        {

            return View();
        }

        [HttpPost]
        [Route("kullanici/sifre")]
        public IActionResult PasswordChange(PasswordChangeViewModel model)
        {

            if (!ModelState.IsValid)
            {
                ViewData["GeneralError"] = Messages.RequiredInput;
                return View(model);
            }

            AppUser user = CurrentUser;

            if (user != null)
            {
                bool exist = _userManager.CheckPasswordAsync(user, model.OldPassword).Result;

                if (exist)
                {
                    var result = _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword).Result;

                    if (result.Succeeded)
                    {
                        ViewData["GeneralSuccess"] = Messages.PasswordChanged;
                        _userManager.UpdateSecurityStampAsync(user);
                        return RedirectToAction("Index", "ManagementPanel");
                    }
                    else
                    {
                        ViewData["GeneralError"] = Messages.GeneralError;
                        return View(model);
                    }
                }
                else
                {
                    ViewData["GeneralError"] = Messages.InputValueNoValid;
                }
            }

            ViewData["GeneralError"] = Messages.GeneralError;
            return View(model);
        }

        [Route("kullanici/duzenle")]
        public IActionResult UserEdit()
        {

            AppUser user = CurrentUser;

            UserEditViewModel model = new UserEditViewModel
            {
                Email = user.Email
            };


            return View(model);
        }

        [HttpPost]
        [Route("kullanici/duzenle")]
        public async Task<IActionResult> UserEdit(UserEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["GeneralError"] = Messages.RequiredInput;
                return View(model);
            }

            AppUser user = CurrentUser;


            user.Email = model.Email;


            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                await _userManager.UpdateSecurityStampAsync(user);
                await _signInManager.SignOutAsync();
                await _signInManager.SignInAsync(user, false);
                ViewData["GeneralSuccess"] = Messages.EmailChanged;
                return RedirectToAction("Index", "ManagementPanel");
            }


            ViewData["GeneralError"] = Messages.GeneralError;
            return View(model);
        }

        [Route("sosyal/isactive")]
        [HttpPost]
        public JsonResult ChangeIsActiveSocial(int id)
        {
            var data = _socialService.GetById(id);
            data.Data.IsActive = !data.Data.IsActive;

            _socialService.Update(data.Data);
            return new JsonResult(true);
        }


        [Route("slider/isactive")]
        [HttpPost]
        public JsonResult ChangeIsActiveSlider(int id)
        {
            var data = _sliderService.GetById(id);
            data.Data.IsActive = !data.Data.IsActive;

            _sliderService.Update(data.Data);
            return new JsonResult(true);
        }

        [Route("sayfa/isactive")]
        [HttpPost]
        public JsonResult ChangeIsActivePage(int id)
        {
            var data = _pageService.GetById(id);
            data.Data.IsActive = !data.Data.IsActive;

            _pageService.Update(data.Data);
            return new JsonResult(true);
        }

        [Route("galeri/isactive")]
        [HttpPost]
        public JsonResult ChangeIsActiveGallery(int id)
        {
            var data = _galleryService.GetById(id);
            data.Data.IsActive = !data.Data.IsActive;

            _galleryService.UpdateIsActive(data.Data);
            return new JsonResult(true);
        }

        [Route("kategori/isactive")]
        [HttpPost]
        public JsonResult ChangeIsActiveCategory(int id)
        {
            var data = _categoryImageService.GetById(id);
            data.Data.IsActive = !data.Data.IsActive;

            _categoryImageService.Update(data.Data);
            return new JsonResult(true);
        }



    }
}
