using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strore_Common.Extensions
{
    public static class EnumerableExtensions
    {
        [DebuggerStepThrough]
        public static void Each<T>(this IEnumerable<T> instance, Action<T> action)
        {
            if (instance != null)
            {
                foreach (T item in instance)
                {
                    action(item);
                }
            }
        }
    }
}
