using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CQSLab.UI.Controllers
{
    public class MiniProfilerController : Controller
    {
        // GET: MiniProfiler
        public ActionResult On()
        {
            var cookie = new HttpCookie("miniprofiler", "on");
            cookie.Expires.AddDays(1);
            HttpContext.Response.Cookies.Add(cookie);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Off()
        {
            if (Request.Cookies["miniprofiler"] != null)
            {
                var cookie = new HttpCookie("miniprofiler")
                {
                    Expires = DateTime.Now.AddDays(-1),
                    Value = "off"
                };
                Response.Cookies.Add(cookie);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}