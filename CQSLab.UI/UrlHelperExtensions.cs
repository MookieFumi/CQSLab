using System;
using System.Web.Mvc;

namespace CQSLab.UI
{
    public static class UrlHelperExtensions
    {
        public static string AbsolutePath(this UrlHelper urlHelper, string relativePath)
        {
            return new Uri(urlHelper.RequestContext.HttpContext.Request.Url, relativePath).ToString();
        }
    }
}