using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Models.Authentication;
using ViewModels.Authentication;

namespace Core
{
    public static class CurrentContext
    {
        [ThreadStatic]
        private static CurrentUserViewModel _user;

        public static CurrentUserViewModel User
        {
            get
            {
                //if(_user == null)
                //{
                //    throw new Exception("Current user has not be initialized");
                //}

                return _user;
            }
            set
            {
                _user = value;
            }
        }

    }
}
