using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.EntityStock
{
    public class ProductSku
    {
        public int SkuId { get; set; }
        public int ProductId { get; set; }
        public string SKU { get; set; }
        public decimal Price { get; set; }
        
        public ICollection<ProductValue> ProductValues { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
