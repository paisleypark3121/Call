using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Call
{
    public class Random
    {
        private static readonly System.Random _random = new System.Random();
        private static readonly object syncLock = new object();
        public static int generate(int min, int max)
        {
            lock (syncLock)
                return _random.Next(min, max);
        }
    }
}
