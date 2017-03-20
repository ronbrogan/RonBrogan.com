using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.BlogItems
{
    public class ListBlogsViewModel
    {
        public int Total { get; set; }
        public int Page { get; set; }

        public IEnumerable<BlogViewModel> Items { get; set; }
    }
}
