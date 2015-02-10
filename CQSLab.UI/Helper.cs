using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

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
    }
}
