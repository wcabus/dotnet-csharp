using System.Collections.Generic;
using System.Linq;

namespace Static
{
    public static class Extensions
    {
        public static bool IsNull<T>(this IEnumerable<T> source)
        {
            return source == null;
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
        {
            return source?.Any() != true;
        }
    }
}