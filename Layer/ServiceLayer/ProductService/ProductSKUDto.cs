using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ProductService
{
    public class ProductSKUDto
    {
        public int ProductId { get; set; }
        public string SKU { get; set; }
        public decimal Price { get; set; } = 0;
    }
}
