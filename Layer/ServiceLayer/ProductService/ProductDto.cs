﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ProductService
{
    public class ProductDto
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Product Name is required")]
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public string ProductDescription { get; set; }


        public string Category { get; set; }

        [Required(ErrorMessage = "Product Category is required")]
        public int CategoryId { get; set; }


        public string Unit { get; set; }

        [Required(ErrorMessage = "Product Unit is required")]
        public int UnitId { get; set; }

        public int SkuId { get; set; }
        [Required(ErrorMessage = "Product SKU is required")]
        public string SKU { get; set; }
        public decimal Price { get; set; } = 0;


    }
}
