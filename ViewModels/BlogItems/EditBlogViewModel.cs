using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ViewModels.BlogItems
{
    public class EditBlogViewModel
    {
        public string Title { get; set; }

        public string TeaserText { get; set; }

        public string ImageUrl { get; set; }

        [AllowHtml]
        public string BodyHtml { get; set; }
    }
}
