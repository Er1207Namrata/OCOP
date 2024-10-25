using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Extensions
{
    public static class StringListExtensions
    {
        public static string ConcatenateAll(this List<string> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            StringBuilder result = new StringBuilder();
            foreach (var item in list)
            {
                result.Append(item);
            }
            return result.ToString();
        }
        public static List<int>ConvertToIntArray(this string str)
        {
            return str.Split(',').Select(int.Parse).ToList();
        }
    }
}
