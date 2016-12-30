using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Authentication
{
    public class CurrentUserViewModel
    {
        public Guid Id { get; set; }

        public string Email { get; set; }
    }
}
