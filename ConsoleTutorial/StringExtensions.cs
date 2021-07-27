using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTutorial
{
    public static class StringExtensions
    {
        public static string[] SplitString(this string value, char valueToSplit)
        {
            return value.Split(valueToSplit);
        }
    }
}
