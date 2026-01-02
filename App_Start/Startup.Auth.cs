using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using HotelBooking.Models.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using HotelBooking.App_Start;

namespace HotelBooking
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            //app.CreatePerOwinContext<UserManager<ApplicationUser>>(CreateUserManager);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext <ApplicationSignInManager>(ApplicationSignInManager.Create);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Admin/Account/Login"),
                ExpireTimeSpan = TimeSpan.FromHours(8)
            });
        }

        private static UserManager<ApplicationUser> CreateUserManager(
           IdentityFactoryOptions<UserManager<ApplicationUser>> options,
                IOwinContext context
           )
        {
            var manager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>())
                );

            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6
            };

            return manager;
        }
    }
}