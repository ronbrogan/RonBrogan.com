using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Security;
using AutoMapper;
using Common.Authentication;
using Core;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RonBrogan.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Models.Authentication;
using Models;
using Models.BlogItems;
using ViewModels.BlogItems;

namespace RonBrogan.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private BroganContext dbContext = new BroganContext();

        [AllowAnonymous]
        [Route("Admin"), HttpGet]
        public ActionResult Index()
        {
            if (CurrentContext.User == null)
                return View();
           
            return Redirect("/Admin/ListBlogs");
        }

        [AllowAnonymous]
        [Route("Admin/Login"), HttpPost]
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

            return Redirect("/Admin/ListBlogs");
        }

        [AllowAnonymous]
        [Route("Admin/Register"), HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
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

        [Route("Admin/ListBlogs"), HttpGet]
        public async Task<ActionResult> ListBlogs(int page = 1)
        {
            var pageCount = 10;
            var total = await dbContext.BlogPosts.CountAsync();
            var blogs = await dbContext.BlogPosts
                .OrderByDescending(b => b.DateCreated)
                .Skip((page - 1) * pageCount).Take(pageCount).ToListAsync();

            return View(new ListBlogsViewModel
            {
                Items = blogs.Select(Mapper.Map<BlogViewModel>),
                Page = page,
                Total = total
            });
        }

        [Route("Admin/CreateBlog"), HttpGet]
        public ActionResult CreateBlog()
        {
            return View();
        }

        [Route("Admin/CreateBlog"), HttpPost]
        public async Task<ActionResult> PostCreateBlog(CreateBlogViewModel blog)
        {
            var newBlog = new Blog()
            {
                DateCreated = DateTime.Now,
                CreatedBy_Id = CurrentContext.User.Id,
                BodyHtml = blog.BodyHtml,
                TeaserText = blog.TeaserText,
                Title = blog.Title
            };

            if (blog.Thumbnail != null)
            {
                var path = Path.Combine(HostingEnvironment.MapPath("~/"), Common.Constants.Folders.StaticAssets);
                Directory.CreateDirectory(path);
                path = Path.Combine(path, (Guid.NewGuid() + Path.GetExtension(blog.Thumbnail.FileName)));
                blog.Thumbnail.SaveAs(path);
                newBlog.ImageUrl = Path.GetFileName(path);
            }

            dbContext.BlogPosts.Add(newBlog);
            await dbContext.SaveChangesAsync();

            return Redirect("/Admin/ListBlogs");
        }

        [Route("Admin/EditBlog/{blogId}"), HttpGet]
        public async Task<ActionResult> EditBlog(Guid blogId)
        {
            var blog = await dbContext.BlogPosts
                .FirstOrDefaultAsync(b => b.Id == blogId);

            return View(Mapper.Map<BlogViewModel>(blog));
        }

        [Route("Admin/EditBlog/{blogId}"), HttpPost]
        public async Task<ActionResult> PostEditBlog(Guid blogId, EditBlogViewModel blog)
        {
            var entity = await dbContext.BlogPosts.FirstOrDefaultAsync();

            dbContext.Entry(entity).CurrentValues.SetValues(blog);
            await dbContext.SaveChangesAsync();

            return Redirect("/Admin/ListBlogs");   
        }
    }
}