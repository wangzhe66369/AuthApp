using System;
using System.Collections.Generic;
using System.Text;

namespace AuthApp.Common.Extensions
{
    public static class EnumerableExtensions
    {
        public static string JoinAsString(this IEnumerable<string> source, string separator)
        {
            return string.Join(separator, source);
        }
    }
}
