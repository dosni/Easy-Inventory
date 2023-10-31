using Microsoft.AspNetCore.Identity;

namespace DataLayer.EntityStock
{
    public class ApplicationRole : IdentityRole
    {

        public string? Description { get; set; }

    }
}
