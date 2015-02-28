using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQSLab.UI
{
    public static class Utilities
    {
        public static void AppendAnonymousObjectToDictionary(Dictionary<string, object> dictionary, object o)
        {
            if (o == null)
            {
                return;
            }
            var pairs = o as Dictionary<string, object>;
            if (pairs != null)
            {
                foreach (var keyValuePair in pairs)
                {
                    if (dictionary.ContainsKey(keyValuePair.Key))
                    {
                        dictionary[keyValuePair.Key] = keyValuePair.Value;
                    }
                    else
                    {
                        dictionary.Add(keyValuePair.Key, keyValuePair.Value);
                    }
                }
            }
            else
            {
                var properties = o.GetType().GetProperties();
                foreach (var propertyInfo in properties)
                {
                    var propertyName = propertyInfo.Name;
                    var value = propertyInfo.GetValue(o, null);
                    if (dictionary.ContainsKey(propertyName))
                    {
                        dictionary[propertyName] = value;
                    }
                    else
                    {
                        dictionary.Add(propertyName, value);
                    }
                }
            }
        }
    }
}
