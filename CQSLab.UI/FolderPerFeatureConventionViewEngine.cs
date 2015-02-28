using System.Web.Mvc;

namespace CQSLab.UI
{
    //http://trycatchfail.com/blog/post/A-View-Engine-for-ASPNET-MVC-Feature-Based-Organized.aspx
    public class FolderPerFeatureConventionViewEngine : RazorViewEngine
    {
        //{1}. Controller
        //{0}. Action
        public FolderPerFeatureConventionViewEngine()
        {
            ViewLocationFormats = new string[]
            {
                "~/Features/{1}/Views/{0}.cshtml",
                "~/Features/{1}/Partial/{0}.cshtml",
                "~/Views/{1}/{0}.cshtml"
            };

            MasterLocationFormats = ViewLocationFormats;
            PartialViewLocationFormats = ViewLocationFormats;
        }

        private static readonly string RootNamespace = typeof(MvcApplication).Namespace;

        private static string GetPath(ControllerContext controllerContext, string viewName)
        {
            //TODO: Cache?
            var controllerType = controllerContext.Controller.GetType();
            var featureFolder = "~" + controllerType.Namespace.Replace(RootNamespace, string.Empty).Replace(".", "/");

            var path = featureFolder + "/Views/" + viewName + ".cshtml";
            return path;
        }

        public override ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
        {
            var path = GetPath(controllerContext, viewName);

            if (VirtualPathProvider.FileExists(path))
            {
                return new ViewEngineResult(CreateView(controllerContext, path, null), this);
            }
            return new ViewEngineResult(new[] { path });
        }

        public override ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName, bool useCache)
        {
            var path = GetPath(controllerContext, partialViewName);

            if (VirtualPathProvider.FileExists(path))
            {
                return new ViewEngineResult(CreateView(controllerContext, path, null), this);
            }
            return new ViewEngineResult(new[] { path });
        }
    }
}
