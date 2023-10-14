using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.StoreServices
{
    public class StoreDto
    {
        public int StoreId { get; set; }

        [Required(ErrorMessage = "Store Name is required")]
        public string StoreName { get; set; }
    }
}
