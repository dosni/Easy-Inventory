using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.EntityStock
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName{ get; set; }
        public string ProductImage { get; set; }
        public string ProductDescription { get; set; }  

        public int CategoryId { get; set; }
        //public decimal Price { get; set; }
        public ProductCategory ProductCategory { get; set; }
        public ProductSku ProductSku { get; set; }
        public ProductProperty productProperty { get; set; }
        public ICollection<Stock> Stocks { get; set; }
    }
}
