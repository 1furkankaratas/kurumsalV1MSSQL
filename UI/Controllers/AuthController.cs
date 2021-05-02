using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Constants;
using Entities.Concrete.MicrosoftIdentity;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using UI.Models;

namespace UI.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [HttpGet]
        [Route("giris")]
        public IActionResult Login()
        {
            return View();

        }

        [HttpPost]
        [Route("giris")]
        public async Task<IActionResult> Login(UserForLoginDto model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["GeneralError"] = Messages.RequiredInput;
                return View(model);
            }
            AppUser user = await _userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {
                await _signInManager.SignOutAsync();
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);

                if (result.Succeeded)
                {

                    await _userManager.ResetAccessFailedCountAsync(user);
                    ViewData["GeneralSuccess"] = Messages.SuccessfulLogin;
                    if (TempData["ReturnUrl"] != null)
                    {
                        return Redirect(TempData["ReturnUrl"].ToString());
                    }
                    return RedirectToAction("Index", "ManagementPanel");
                }

            }

            ViewData["GeneralError"] = Messages.UserNotFound;
            return View(model);
        }

        [HttpGet]
        [Route("cikis")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            ViewData["GeneralSuccess"] = Messages.UserLogout;
            return RedirectToAction("Login", "Auth");

        }


    }
}
