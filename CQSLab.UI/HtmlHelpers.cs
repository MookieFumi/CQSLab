using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using CQSLab.UI.ViewModels;

namespace CQSLab.UI
{
    public static class HtmlHelpers
    {
        public static MvcHtmlString GetFormGroup<TModel, TProperty>(
    this HtmlHelper<TModel> htmlHelper,
    Expression<Func<TModel, TProperty>> expr) where TModel : MonthlyDataVM
        {
            var value = String.Format(@"
                <div class='form-group'>
                    {0}
                    <div class='col-sm-10'>
                        {1}
                    </div>
                </div>
            ", 
             htmlHelper.LabelFor(expr, new { @class = "col-sm-2 control-label"}),
             htmlHelper.EditorFor(expr, new { @class = "form-control", placeholder = "Name" }));
            return new MvcHtmlString(value);
        }
    }
}