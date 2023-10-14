using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.EntityStock
{
    public class ProductValue
    {
        public string SKU { get; set; }
        public int PropertyId { get; set; }
        public string PropertyValue { get; set; }
        public ProductSku ProductSku { get; set; }
        public ProductProperty ProductProperty { get; set; }

    }
}
