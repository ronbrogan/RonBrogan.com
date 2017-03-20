using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using Models.BlogItems;
using System.Data.Entity;
using System.Threading.Tasks;
using AutoMapper;
using ViewModels.BlogItems;

namespace RonBrogan.Controllers
{
    public class HomeController : Controller
    {
        private BroganContext db = new BroganContext();

        public async Task <ActionResult> Index()
        {
            var posts = await db.BlogPosts
                .OrderByDescending(b => b.DateCreated)
                .Take(5).ToListAsync();

            return View(posts.Select(Mapper.Map<BlogViewModel>));
        }

        [Route("about")]
        public ActionResult About()
        {
            return View();
        }
    }
}