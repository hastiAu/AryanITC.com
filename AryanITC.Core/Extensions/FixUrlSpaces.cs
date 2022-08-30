using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AryanITC.Core.Extensions
{

    public static class FixUrlSpaces
    {
        public static string FixUrlSpacesWithDash(this string url)
        {
            return url.ToLower().Replace(" ", "-");
        }
    }
}
