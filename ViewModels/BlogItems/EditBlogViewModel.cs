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

        public string Metadata { get; set; }

        public bool Public { get; set; }

        public string[] Categories { get; set; }
    }
}
