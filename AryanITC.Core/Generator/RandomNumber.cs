using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AryanITC.Core.Generator
{
    public class RandomNumber
    {
        public static int Random(int min, int max)
        {
            var rand = new Random();
            return rand.Next(min, max);
        }
    }
}
