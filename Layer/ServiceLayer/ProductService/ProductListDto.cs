using System.ComponentModel.DataAnnotations;

namespace ServiceLayer.ProductService
{
    public class ProductListDto
    {
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Product Name is required")]
        public string ProductName { get; set; }


    }


}
