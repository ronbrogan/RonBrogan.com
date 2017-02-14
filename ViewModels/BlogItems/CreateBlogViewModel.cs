using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ViewModels.BlogItems
{
    public class CreateBlogViewModel
    {
        public string Title { get; set; }

        public string TeaserText { get; set; }

        public HttpPostedFileBase Thumbnail { get; set; }

        [AllowHtml]
        public string BodyHtml { get; set; }
    }
}
