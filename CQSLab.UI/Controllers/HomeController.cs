using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CQSLab.Services;
using StackExchange.Profiling;

namespace CQSLab.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUsersService _usersService;

        public HomeController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        public async Task<ViewResult> Index()
        {
            var profiler = MiniProfiler.Current;
            profiler.Step("MiniProfiler step");

            var userId = Helper.GetUserId();
            ViewBag.IsFilledPersonalData = await _usersService.IsFilledPersonalData(userId);
            ViewBag.IsFilledStudiesData = await _usersService.IsFilledStudiesData(userId);

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}