using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace CQSLab.UI
{
    public static class HtmlExtensions
    {
        public static HtmlString PostLink(
            this HtmlHelper htmlHelper,
            string value,
            string actionName,
            string controllerName,
            object routeValues,
            object htmlAttributes,
            string onclick)
        {
            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            var action = urlHelper.Action(actionName, controllerName, routeValues);
            var form = new TagBuilder("form");
            form.MergeAttribute("action", action);
            form.MergeAttribute("method", FormMethod.Post.ToString());
            form.MergeAttribute("style", "display: inline-block;");
            var a = new TagBuilder("a");
            a.SetInnerText(value);
            if (htmlAttributes != null)
            {
                a.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            }
            a.Attributes.Add("href", "#");
            a.Attributes.Add("onclick", onclick + "$(this).parent()[0].submit();");
            form.InnerHtml = a.ToString();
            var formToken = AntiForgery.GetHtml();
            form.InnerHtml += formToken.ToString();
            return new HtmlString(form.ToString());
        }
    }
}