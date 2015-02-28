using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using StackExchange.Profiling;
using StackExchange.Profiling.EntityFramework6;

namespace CQSLab.UI
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
            ViewEngines.Engines.Add(new FolderPerFeatureConventionViewEngine());

            MiniProfilerEF6.Initialize();
        }

        protected void Application_BeginRequest()
        {
            var cookie = Request.Cookies[Strings.CookieMiniProfiler];
            if (cookie != null && cookie.Value == Strings.On)
            {
                MiniProfiler.Start();
            }
        }

        protected void Application_EndRequest()
        {
            MiniProfiler.Stop();
        }
    }
}
