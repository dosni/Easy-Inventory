using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.EntityStock
{
    public class ProductCategory
    {
        public int CategoryId { get; set; }
        public string Category { get; set; }
        public ICollection<Product> Products{ get; set; }
    }
}
