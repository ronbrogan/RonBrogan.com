using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;
using System.Web.Mvc.Filters;
using Microsoft.AspNet.Identity.EntityFramework;
using Models.Authentication;
using Newtonsoft.Json;
using ViewModels.Authentication;

namespace Core.Filters
{
    public class CurrentContextFilter : IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            var ctx = filterContext.HttpContext;
            var authenticated = ctx.User?.Identity?.IsAuthenticated;
            if (authenticated.HasValue && authenticated.Value)
            {
                var user = ((ClaimsPrincipal)ctx.User);
                var currentUser = JsonConvert.DeserializeObject<CurrentUserViewModel>(user.Claims.First(c => c.Type == "CurrentUser").Value);
                CurrentContext.User = currentUser;
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            
        }
    }
}
