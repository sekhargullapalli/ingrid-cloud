using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterMind.WebApp
{
    internal static class SiteExtensions
    {
        internal static Random RandomGenerator = new Random();

        internal static T RandomEnum<T>()
        {
            Type type = typeof(T);
            Array values = Enum.GetValues(type);
            lock (RandomGenerator)
            {
                object value = values.GetValue(RandomGenerator.Next(values.Length));
                return (T)Convert.ChangeType(value, type);
            }
        }
    }
}
