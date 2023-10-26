namespace DataLayer.EntityStock
{
    public class ProductUnit
    {
        public int UnitId { get; set; }
        public string Unit { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
