using System;

namespace ViewModels.BlogItems
{
    public class CategoryViewModel
    {
        public Guid Id { get; set; }

        public Guid Blog_Id { get; set; }

        public string CategoryName { get; set; }
    }
}
