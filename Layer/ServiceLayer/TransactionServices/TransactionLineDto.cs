using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.TransactionServices
{
    public class TransactionLineDto
    {
        public int LineId { get; set; }
        public string ProductName { get; set; }
        public string TransactionType { get; set; }
        public string SKU { get; set; }
        public int SkuId { get; set; }
        public int StoreId { get; set; }
        public decimal Qty { get; set; }
        public decimal QtyCounted { get; set; }
        public decimal? QtyAvaiable { get; set; }
        public decimal Price { get; set; }
    }
}
