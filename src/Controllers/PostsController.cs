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
        public async Task<ActionResult> Index(string category)
        {
            var postQuery = db.BlogPosts
                .Include(b => b.Categories)
                .Where(b => b.Public);

            if(string.IsNullOrWhiteSpace(category) == false)
            {
                postQuery = postQuery.Where(b => b.Categories.Any(c => c.CategoryName == category));
            }

            var posts = await postQuery.Take(5).ToListAsync();

            var categories = await db.Categories
                .GroupBy(c => c.CategoryName)
                .Select(c => c.Key)
                .ToListAsync();
                
            return View(new ExplorePostsViewModel()
            {
                Blogs = posts.Select(Mapper.Map<BlogViewModel>),
                Categories = categories
            });
        }

        [Route("posts/{blogId}")]
        public async Task<ActionResult> Details(Guid blogId)
        {
            var post = await db.BlogPosts
                .Include(b => b.Categories)
                .Where(b => b.Public)
                .FirstOrDefaultAsync(b => b.Id == blogId);

            return View("Details", Mapper.Map<BlogViewModel>(post));
        }
    }
}