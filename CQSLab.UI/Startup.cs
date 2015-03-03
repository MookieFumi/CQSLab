using System;
using CQSLab.UI;
using CQSLab.UI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Twitter;
using Owin;

[assembly: OwinStartup(typeof(Startup))]
namespace CQSLab.UI
{
    public class Startup
    {
        public static Func<UserManager<ApplicationUser>> UserManagerFactory { get; private set; }

        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }

        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        private static void ConfigureAuth(IAppBuilder app)
        {
            app.CreatePerOwinContext(SecurityContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);
            
            // Enable the application to use a cookie to store information for the signed in user
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });
            // Use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            app.UseTwitterAuthentication(new TwitterAuthenticationOptions
            {
                ConsumerKey = "CDtAUdpC0nixBnsrGxklPxxEi",
                ConsumerSecret = "Rg1ynGXqq3K8Pj3gwTifoL8neI4CVKf5KjkB251KEqwsEbTXcY"
            });

            // Configure the user manager
            // We use a delegate here so we can acess the IBuilder
            // Then we bind this delegate to UserManager<AppUserModel> in Ninject
            UserManagerFactory = () =>
            {
                var usermanager = new UserManager<ApplicationUser>(
                    new UserStore<ApplicationUser>(new SecurityContext()));

                // allow alphanumeric characters in username
                usermanager.UserValidator = new UserValidator<ApplicationUser>(usermanager)
                {
                    AllowOnlyAlphanumericUserNames = false
                };

                //usermanager.PasswordValidator = new PasswordValidator
                //{
                //    RequiredLength = 6,
                //    RequireNonLetterOrDigit = true,
                //    RequireDigit = false,
                //    RequireLowercase = false,
                //    RequireUppercase = false
                //};

                //usermanager.UserLockoutEnabledByDefault = true;
                //usermanager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
                //usermanager.MaxFailedAccessAttemptsBeforeLockout = 5;
                
                return usermanager;
            };
        }
    }
}
