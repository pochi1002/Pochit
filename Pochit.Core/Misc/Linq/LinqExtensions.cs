using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pochit.Core.Misc.Linq
{
    [DebuggerStepThrough]
    public static class LinqExtensions
    {
        public static void ForEach<T>(this IReadOnlyList<T> self, Action<T> act)
        {
            foreach (var each in self) act.Invoke(each);
        }
    }
}
