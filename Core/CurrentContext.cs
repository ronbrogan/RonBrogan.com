using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Models.Authentication;
using ViewModels.Authentication;
using System.Web;

namespace Core
{
    public static class CurrentContext
    {
        private static string HttpContextKey = "CurrentUser";


        public static CurrentUserViewModel User
        {
            get
            {
                return (CurrentUserViewModel)HttpContext.Current.Items[HttpContextKey];
            }
            set
            {
                HttpContext.Current.Items[HttpContextKey] = value;
            }
        }

    }
}
