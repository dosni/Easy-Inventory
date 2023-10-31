using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.EntityStock
{
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }
        public int StoreId { get; set; }

    }
}
