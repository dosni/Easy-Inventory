using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.EntityStock
{
    public class ProductTransaction
    {
        public int TransactionId { get; set; }
        public string TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
        public int ProductId { get; set; }
        public int StoreId { get; set; }
        public decimal Qty { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }

        public Product Product { get; set; }
        public Store Store { get; set; }

      
    }
}
