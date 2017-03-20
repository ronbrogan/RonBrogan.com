using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Threading.Tasks;
using AutoMapper;
using ViewModels.BlogItems;

namespace RonBrogan.Controllers
{
    public class PostsController : Controller
    {
        private BroganContext db = new BroganContext();
        // GET: Posts
        public async Task<ActionResult> Index()
        {
            var posts = await db.BlogPosts
                .Take(5).ToListAsync();

            return View(posts.Select(Mapper.Map<BlogViewModel>));
        }

        [Route("posts/{blogId}")]
        public async Task<ActionResult> Details(Guid blogId)
        {
            var post = await db.BlogPosts
                .FirstOrDefaultAsync(b => b.Id == blogId);

            return View("Details", Mapper.Map<BlogViewModel>(post));
        }
    }
}