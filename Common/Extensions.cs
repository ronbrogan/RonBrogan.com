using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
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

        public static Guid GetUserIdGuid(this IIdentity identity)
        {
            Guid result = Guid.Empty;
            Guid.TryParse(identity.GetUserId(), out result);
            return result;
        }
    }
}
