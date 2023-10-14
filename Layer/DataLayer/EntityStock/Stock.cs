using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.EntityStock
{
    public class Stock
    {
        public int StoreId { get; set; }
        public int ProductId { get; set; }
        public decimal qty { get; set; }
        public Product Product { get; set; }
        public Store Store { get; set; }

    }
}
