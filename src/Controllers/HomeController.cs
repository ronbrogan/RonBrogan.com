using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using Models.BlogItems;
using System.Data.Entity;

namespace RonBrogan.Controllers
{
    public class HomeController : Controller
    {
        private BroganContext db = new BroganContext();

        public ActionResult Index()
        {
            var posts = db.BlogPosts
                .Include(b => b.CreatedBy)
                .Take(5).ToList();

            return View(posts);
        }

        [Route("about")]
        public ActionResult About()
        {
            return View();
        }
    }
}