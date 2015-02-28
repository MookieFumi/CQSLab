using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CQSLab.UI
{
    public static class SelectListHelper
    {
        public static IEnumerable<SelectListItem> GetFromDictionary(Dictionary<int, string> items)
        {
            return items.Select(item => new SelectListItem
            {
                Value = item.Key.ToString(),
                Text = item.Value
            }).ToList();
        }
    }
}
