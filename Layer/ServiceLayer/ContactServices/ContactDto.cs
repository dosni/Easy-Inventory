using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ContactServices
{
    public class ContactDto
    {
        public int ContactId { get; set; }
        //[Required(ErrorMessage = "Contact Type is required")]
        public string type { get; set; }
        [Required(ErrorMessage = "Name  is required")]
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
    }
}
