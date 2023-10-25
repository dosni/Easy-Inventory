using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.EntityStock
{
    public class Store
    {
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public ICollection<Stock> Stocks { get; set; }
        public ICollection<ProductTransaction> ProductTransactions { get; set; }

    }
}
