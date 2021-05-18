using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anywherePIP
{
    public class BitService
    {
        public static List<T> GetFlags<T>(int flags) where T : struct, Enum
        {
            return
                Enum.GetValues(typeof(T))
                .OfType<T>()
                .ToList()
                .Where(x => (flags & Convert.ToInt64(x)) != 0)
                .ToList(); ;
        }
    }
}
