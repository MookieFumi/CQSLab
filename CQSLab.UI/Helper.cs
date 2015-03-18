using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.ModelBinding;
using CQSLab.UI.Features.Account.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using ModelStateDictionary = System.Web.Mvc.ModelStateDictionary;

namespace CQSLab.UI
{
    public static class Helper
    {
        public static string GetErrorsFromModelState(ModelStateDictionary modelState)
        {
            return string.Join("<BR>",
                    modelState.Values.Where(e => e.Errors.Any())
                    .SelectMany(e => e.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToArray());
        }

        public static bool IsAuthenticated()
        {
            return HttpContext.Current.User.Identity.IsAuthenticated;
        }

        public static string GetUserId()
        {
            return HttpContext.Current.User.Identity.GetUserId();
        }

        public static string GetIdentityName()
        {
            return HttpContext.Current.User.Identity.Name;
        }

        public static bool IsMiniProfilerActivated()
        {
            var cookie = HttpContext.Current.Request.Cookies[Strings.CookieMiniProfiler];
            if (cookie != null && cookie.Value == Strings.On)
            {
                return true;
            }
            return false;
        }

        public static string GetUserProfileImage()
        {
            var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = userManager.FindByName(Helper.GetIdentityName());

            if (user.Image!=null)
            {
                return System.Convert.ToBase64String(user.Image);
            }

            return string.Empty;
        }
    }
}