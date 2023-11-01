using System.ComponentModel.DataAnnotations;

namespace ServiceLayer.ProductTransactionServices
{
    public class ProductTransactionDto
    {

        public int TransactionId { get; set; }
        public string TransactionType { get; set; } // 1=I 2=A 3. R 4. D
        public DateTime? TransactionDate { get; set; }
        [Required(ErrorMessage = "Product Name is required")]

        //public int ProductId { get; set; }

        public int SkuId { get; set; }
        public string ProductName { get; set; }
        [Required(ErrorMessage = "Location is required")]
        public int StoreId { get; set; }

        public string StoreName { get; set; }
        public bool IsPosted { get; set; } = true;

        [Range(1.0, Double.MaxValue, ErrorMessage = "The field {0} must be greater or equal  {1}.")]
        public decimal Qty { get; set; }

        [Range(0.0, Double.MaxValue, ErrorMessage = "The field  Cost must be greater or equal {1}.")]
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
