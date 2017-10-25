using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.BlogItems
{
    public class ExplorePostsViewModel
    {
        public IEnumerable<string> Categories { get; set; }
        public IEnumerable<BlogViewModel> Blogs { get; set; }
    }
}
