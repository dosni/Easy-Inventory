using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.TransactionServices
{
    public class ProductDisplayDto
    {
   
        public string ProductName { get; set; }
        public string SKU { get; set; }
        public int SkuId { get; set; }
        public decimal Price { get; set; }
        public decimal Qty { get; set; } = 1;
    }
}
