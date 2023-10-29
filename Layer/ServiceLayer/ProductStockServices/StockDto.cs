using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ProductStockServices
{
    public class StockDto
    {
        public int StoreId { get; set; }
        public int SkuId { get; set; }
        public decimal qty { get; set; }
    }
}
