namespace ServiceLayer.ProductService
{
    public class ProductSKUDto
    {
        public int SkuId { get; set; }
        public int ProductId { get; set; }
        public string SKU { get; set; }
        public decimal Price { get; set; } = 0;
    }

}
