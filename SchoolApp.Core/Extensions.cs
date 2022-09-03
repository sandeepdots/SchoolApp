using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Core
{
    public static class Extensions
    {
        public static void AddOrReplace(this IDictionary<string, object> dictionaryContainer, string key, object value)
        {
            if (dictionaryContainer.ContainsKey(key))
            {
                dictionaryContainer[key] = value;
            }
            else
            {
                dictionaryContainer.Add(key, value);
            }
        }
    }
}
