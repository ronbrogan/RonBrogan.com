using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.BlogItems
{
    public class Category : EntityBase
    {
        [ForeignKey(nameof(Blog))]
        public Guid Blog_Id { get; set; }

        public Blog Blog { get; set; }

        [Index]
        [MaxLength(255)]
        public string CategoryName { get; set; }
    }
}
