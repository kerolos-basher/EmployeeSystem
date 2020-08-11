using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Idintitycorepro.Data;
using Idintitycorepro.Models;
using Idintitycorepro.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Idintitycorepro.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<AccountController> logger;

        public AccountController(UserManager<ApplicationUser> userManager 
            , SignInManager<ApplicationUser> signInManager,
            ILogger<AccountController> logger)
        {
            _userManager = userManager ;
          _signInManager = signInManager;
            this.logger = logger;
        }
        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("AllEmployees", "Employee");
        }
        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPassword model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    if (await _userManager.IsEmailConfirmedAsync(user))
                    {
                        return Redirect("ResetPassword");
                    }
                }
                ModelState.AddModelError(string.Empty, "Emil not found");
                return View(model);
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult ResetPassword()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    UserName = model.Email,
                    Email = model.Email,
                    City = model.City
                };
                user.EmailConfirmed = true;
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //---------------------------malho4 lazma
                    //var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //var confirmationlink = Url.Action("confirmEmail", "Account",
                    //            new { userId = user.Id, token = token }, Request.Scheme);

                    //logger.Log(LogLevel.Warning, confirmationlink);

                    //if (_signInManager.IsSignedIn(User) && User.IsInRole("Admin"))
                    //{
                    //    return RedirectToAction("AllEmployees", "Employee");
                    //}
                    //ViewBag.ErrorTitle = "Registration Succesful but you must confirm your Email First ";
                    //return View("Error");
                    //---------------------------
                    return RedirectToAction("AllEmployees", "Employee");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }


        //[HttpPost]
        //[AllowAnonymous]
        //public async Task<IActionResult> confirmEmail(string userId , string token)
        //{
        //    if (userId == null || token == null)
        //    {
        //        return RedirectToAction("AllEmployees", "Employee");
        //    }
        //    var user = await _userManager.FindByIdAsync(userId);
        //    if (user == null)
        //    {
        //        ViewBag.ErrorTitle = " in valid user ";
        //        return View("Error");
        //    }
        //     var result =  await _userManager.ConfirmEmailAsync(user, token);
        //    if (result.Succeeded)
        //    {
        //        return View();
        //    }
        //    ViewBag.ErrorTitle = " can not confirm Email ";
        //    return View("Error");
        //}


            public async Task<IActionResult> Login(string ReturnUrl)
        {
            var loginviewmodel = new LoginViewModel()
            {
                returnUrl = ReturnUrl,
              ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };
            return View(loginviewmodel);
        }



        [HttpPost]
        [AllowAnonymous]
        public  IActionResult ExternalLogin(string provider, string returnUrl)
        {
            var redirecturl = Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl });
            var proprties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirecturl);
            return new ChallengeResult(provider, proprties);
        }
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null ,string remoteerror = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            var loginviewmodel = new LoginViewModel()
            {
                returnUrl = returnUrl,
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };

            if (remoteerror != null)
            {
                ModelState.AddModelError(string.Empty, "ssssdserror");
                return View("Login", loginviewmodel);
            }

            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ModelState.AddModelError(string.Empty, "Error Loading External Login");
                return View("Login", loginviewmodel);
            }


            var Email = info.Principal.FindFirstValue(ClaimTypes.Email);
            ApplicationUser user = null;
            if (Email != null)
            {
                user = await _userManager.FindByEmailAsync(Email);
            }
            //---------------------------
            if (user != null && !user.EmailConfirmed)
            {
                ModelState.AddModelError(string.Empty, "Email not confirmed yet");
                return View("Login", loginviewmodel);
            }
            //---------------------------
            var sininresult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider,
                                                 info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            if (sininresult.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
               
                if (Email != null)
                {
                     user = await _userManager.FindByEmailAsync(Email);
                    if (user == null)
                    {
                        user = new ApplicationUser()
                        {
                            UserName = info.Principal.FindFirstValue(ClaimTypes.Email),
                            Email = info.Principal.FindFirstValue(ClaimTypes.Email)

                    };
                        user.EmailConfirmed = true;
                        await _userManager.CreateAsync(user);
                        //---------------------------
                        //var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        //var confirmationlink = Url.Action("confirmEmail", "Account",
                        //            new { userId = user.Id, token = token }, Request.Scheme);

                        //logger.Log(LogLevel.Warning, confirmationlink);

                        //if (_signInManager.IsSignedIn(User) && User.IsInRole("Admin"))
                        //{
                        //    return RedirectToAction("AllEmployees", "Employee");
                        //}
                        //ViewBag.ErrorTitle = "Registration Succesful but you must confirm your Email First we emaild you ";
                        //return View("Error");
                        //---------------------------
                    }
                    await _userManager.AddLoginAsync(user,info);
                    await _signInManager.SignInAsync(user, isPersistent:false);
                    return LocalRedirect(returnUrl);
                }
                ViewBag.ErrorTitle = "cant login";
                return View("Error");
            }
        }
     
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model,string returnurl)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                //---------------------------
                if (user != null && !user.EmailConfirmed && (
                                        await _userManager.CheckPasswordAsync(user, model.Password)))
                {
                    ModelState.AddModelError(string.Empty, "Email not confirmed yet");
                    return View(model);
                }
                //------------------------------
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnurl) && Url.IsLocalUrl(returnurl))
                    {
                        return Redirect(returnurl);
                       // return LocalRedirect(returnurl);
                    }
                    else
                    {

                        return RedirectToAction("index", "home");
                    }
                }
               
                    ModelState.AddModelError(string.Empty,"invalid pas our user name" );
                
            }
            return View(model);
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
