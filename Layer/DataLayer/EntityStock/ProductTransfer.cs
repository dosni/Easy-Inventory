using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.EntityStock
{
    public class ProductTransfer
    {
        public int TransactionId { get; set; }
        public DateTime TransactionDate { get; set; }
        public int SkuId { get; set; }
        public int StoreIdFrom { get; set; }
        public int StoreIdTo { get; set; }
        public decimal Qty { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }

        public ProductSku ProductSku { get; set; }
     

    }
}
