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
        public int UnitId { get; set; }


        public ProductUnit ProductUnit { get; set; }
        public ProductCategory ProductCategory { get; set; }

     
        
        public ICollection<ProductSku> ProductSkus { get; set; }
        //public ICollection<Stock> Stocks { get; set; }
        //public ICollection<ProductTransaction> ProductTransactions { get; set; }
        //public ICollection<ProductTransfer> ProductTransfers { get; set; }


       

    }
}
