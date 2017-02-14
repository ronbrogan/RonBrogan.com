using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using AutoMapper;
using ViewModels.BlogItems;

namespace RonBrogan.Controllers
{
    public class PostsController : Controller
    {
        private BroganContext db = new BroganContext();
        // GET: Posts
        public ActionResult Index()
        {
            var posts = db.BlogPosts
                .Take(5).ToList();

            return View(posts.Select(Mapper.Map<BlogViewModel>));
        }

        [Route("posts/{blogId}")]
        public ActionResult Details(Guid blogId)
        {
            var post = db.BlogPosts
                .FirstOrDefault(b => b.Id == blogId);

            return View("Details", Mapper.Map<BlogViewModel>(post));
        }
    }
}