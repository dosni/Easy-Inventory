﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.EntityStock
{
    public class Stock
    {
        public int StoreId { get; set; }
        public int SkuId { get; set; }
        public decimal qty { get; set; }
        public ProductSku ProductSku { get; set; }
        public Store Store { get; set; }

    }
}
