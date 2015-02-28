using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Humanizer;

namespace CQSLab.UI.Controllers
{
    public class MiniProfilerController : Controller
    {
        private const int ONE_DAY = 1;

        // GET: MiniProfiler
        public ActionResult On()
        {
            var cookie = new HttpCookie(Strings.CookieMiniProfiler, Strings.On);

            cookie.Expires.AddDays(ONE_DAY);
            HttpContext.Response.Cookies.Add(cookie);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Off()
        {
            var cookie = Request.Cookies[Strings.CookieMiniProfiler];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(-ONE_DAY);
                cookie.Value = Strings.Off;
                Response.Cookies.Add(cookie);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}