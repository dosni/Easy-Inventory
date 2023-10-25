using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ProductTransactionServices
{
    public class TransactionDto
    {
      
        public int TransactionId { get; set; }
        public string TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Product Name is required")]
        public string ProductName { get; set; }

        public int StoreId { get; set; }
        [Required(ErrorMessage = "Lokai is required")]
        public string StoreName { get; set; }
        public decimal Qty { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
