using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using HotelBooking.Models.Identity;
using HotelBooking.App_Start;
using HotelBooking.Models;

namespace HotelBooking.Areas.Admin.Controllers
{
    
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        #region Manager helpers 
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager
                    ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }

        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager
                    ?? HttpContext.GetOwinContext()?.GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        private IAuthenticationManager AuthenticationManager
            => HttpContext.GetOwinContext().Authentication;

        #endregion

        // =========================
        // LOGIN
        // =========================

        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await SignInManager.PasswordSignInAsync(
                model.Email,
                model.Password,
                model.RememberMe,
                shouldLockout: false);

            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);

                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Email hoặc mật khẩu không đúng");
                    return View(model);
            }
        }

        // =========================
        // LOGOUT
        // =========================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login", "Account");
        }

        // =========================
        // HELPERS
        // =========================

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index", "Dashboard");
        }
        // GET: Admin/Account
        //public ActionResult Index()
        //{
        //    return View();
        //}
    }
}