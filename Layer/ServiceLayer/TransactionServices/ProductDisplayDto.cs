namespace ServiceLayer.TransactionServices
{
    public class ProductDisplayDto
    {

        public string ProductName { get; set; }
        public string SKU { get; set; }
        public int SkuId { get; set; }
        public int? StoreId { get; set; }
        public decimal Price { get; set; }
        public decimal Qty { get; set; } = 1;
        public decimal QtyCounted { get; set; } = 0;
        public decimal? QtyAvaiable { get; set; }

    }
}