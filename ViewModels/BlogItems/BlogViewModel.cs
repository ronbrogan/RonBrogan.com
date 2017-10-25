using System;
using System.Web.Mvc;
using ViewModels.Authentication;

namespace ViewModels.BlogItems
{
    public class BlogViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string TeaserText { get; set; }

        public string ImageUrl { get; set; }

        [AllowHtml]
        public string BodyHtml { get; set; }

        public DateTime DateCreated { get; set; }

        public UserSimpleViewModel CreatedBy { get; set; }

        public string Metadata { get; set; }

        public bool Public { get; set; }

        public string[] Categories { get; set; }
    }
}
