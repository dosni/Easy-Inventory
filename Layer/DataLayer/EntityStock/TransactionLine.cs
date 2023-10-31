namespace DataLayer.EntityStock
{
    public class TransactionLine
    {
        public int LineId { get; set; }
        public int TransactionId { get; set; }
        public int SkuId { get; set; }
        public int StoreId { get; set; }
        public decimal Qty { get; set; }
        public decimal Price { get; set; }

        public ProductSku ProductSku { get; set; }
        public Store Store { get; set; }
        public Transaction Transaction { get; set; }

    }
}
