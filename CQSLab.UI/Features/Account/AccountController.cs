using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using CQSLab.Entities;
using CQSLab.UI.Features.Account.ViewModels;
using CQSLab.UI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace CQSLab.UI.Features.Account
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IAuthenticationManager _authenticationManager;

        public AccountController(IAuthenticationManager authenticationManager)
        {
            _authenticationManager = authenticationManager;
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View(_authenticationManager.GetExternalAuthenticationTypes());
        }

        //
        // GET: /Account/EditProfile
        public ActionResult EditProfile( bool success = false)
        {
            if (success)
            {
                ViewBag.Success = true;
            }

            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = userManager.FindByName(Helper.GetIdentityName());
            Mapper.CreateMap<ApplicationUser, ApplicationUserVM>();
            var viewModel = Mapper.Map<ApplicationUser, ApplicationUserVM>(user);

            return View(viewModel);
        }

        //
        // POST: /Account/EditProfile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(ApplicationUserVM viewModel)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = userManager.FindByName(Helper.GetIdentityName());
            Mapper.CreateMap<ApplicationUserVM, ApplicationUser>();
            Mapper.Map(viewModel, user);
            userManager.UpdateAsync(user);

            return RedirectToAction("EditProfile", "Account");
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await _authenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("ExternalLoginFailure");
            }

            //Sign in the user with this external login provider if the user already has a login
            var result = await HttpContext.GetOwinContext().Get<ApplicationSignInManager>().ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.Failure:
                    var email = string.Format("{0}@{1}.com", loginInfo.DefaultUserName, loginInfo.Login.LoginProvider.ToLower());
                    var user = new ApplicationUser { UserName = loginInfo.DefaultUserName, ProviderName = loginInfo.Login.LoginProvider, Email = email };
                    var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                    var identityResult = await userManager.CreateAsync(user);
                    if (identityResult.Succeeded)
                    {
                        identityResult = await userManager.AddLoginAsync(user.Id, loginInfo.Login);
                        if (identityResult.Succeeded)
                        {
                            await HttpContext.GetOwinContext().Get<ApplicationSignInManager>().SignInAsync(user, isPersistent: false, rememberBrowser: false);
                            return RedirectToLocal(returnUrl);
                        }
                    }
                    return View("ExternalLoginFailure");
            }

            return RedirectToAction("Index", "Home");
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            _authenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //if (_userManager != null)
                //{
                //    _userManager.Dispose();
                //    _userManager = null;
                //}

                //if (_signInManager != null)
                //{
                //    _signInManager.Dispose();
                //    _signInManager = null;
                //}
            }

            base.Dispose(disposing);
        }

        #region Helpers

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        #endregion
    }
}