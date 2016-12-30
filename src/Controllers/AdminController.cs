using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Core.Authentication;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RonBrogan.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Models.Authentication;
using Models;

namespace RonBrogan.Controllers
{
    public class AdminController : Controller
    {
        private BroganContext dbContext = new BroganContext();

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        [Route("Admin/Login")]
        public async Task<ActionResult> PostLogin(LoginViewModel data)
        {
            var userManager = new UserManager<User, Guid>(new UserStore<User, GuidRole, Guid, GuidUserLogin, GuidUserRole, GuidUserClaim>(dbContext));
            var user = userManager.Find(data.Email, data.Password);

            if (user == null)
                throw new Exception("Could not login.");

            var owin = Request.GetOwinContext();
            var authManager = owin.Authentication;

            var identity = await user.GenerateUserIdentityAsync(userManager);
            authManager.SignIn(new AuthenticationProperties() { IsPersistent = true }, identity);

            return Redirect("/");
        }

        [Route("Admin/Register"), HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [Route("Admin/Register"), HttpPost]
        public async Task<ActionResult> PostRegister(RegisterViewModel data)
        {
#if !DEBUG
            throw new Exception("Registration disabled");
#endif
            var user = new User()
            {
                Email = data.Email,
                EmailConfirmed = false,
                UserName = data.Email,
                //Id = Guid.NewGuid()
            };

            var manager = Request.GetOwinContext().GetUserManager<UserManager>();
            var result = await manager.CreateAsync(user, data.Password);

            if (!result.Succeeded)
                throw new Exception($"Unable to register user.{string.Join("\r\n", result.Errors)}");

            return Redirect("/");
        }
    }
}