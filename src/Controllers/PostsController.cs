using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace RonBrogan.Controllers
{
    public class PostsController : Controller
    {
        private BroganContext db = new BroganContext();
        // GET: Posts
        public ActionResult Index()
        {
            var posts = db.BlogPosts
                .Include(b => b.CreatedBy)
                .Take(5).ToList();

            return View(posts);
        }

        [Route("posts/{blogId}")]
        public ActionResult Details(Guid blogId)
        {
            var post = db.BlogPosts
                .Include(b => b.CreatedBy)
                .FirstOrDefault(b => b.Id == blogId);

            return View("Details", post);
        }
    }
}