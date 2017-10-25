using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.BlogItems
{
    public class Blog : EntityBase
    {
        [MaxLength(150)]
        public string Title { get; set; }

        [MaxLength(1000)]
        public string TeaserText { get; set; }

        [MaxLength(255)]
        public string ImageUrl { get; set; }

        [MaxLength(160)]
        public string Metadata { get; set; }

        public string BodyHtml { get; set; }

        public bool Public { get; set; }

        public ICollection<Category> Categories { get; set; }
    }
}
