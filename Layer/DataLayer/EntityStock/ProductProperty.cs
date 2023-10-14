using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.EntityStock
{
    public class ProductProperty
    {
        public int ProductId { get; set; }
        public int PropetyId { get; set; }
        public string PropertyName { get; set; }
        public ICollection<ProductValue> ProductValues { get; set; }
    }
}
