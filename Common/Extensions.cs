﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Common
{
    public static class Extensions
    {
        public static string DeCamelCase(this string input)
        {
            return Regex.Replace(input, "([a-z])([A-Z])", "$1 $2");
        }
    }
}
