using DataLayer.Context;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.ProductService;

namespace ServiceLayer.TransactionServices.Concrete
{
    public class TransactionLineServices
    {
        private readonly StockContext _context;

        public TransactionLineServices(StockContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ProductDisplayDto>?> GetProductListAsycn()
        {
            try
            {
                var query = (from o in _context.Products
                             select new ProductDisplayDto
                             {
                                 ProductName = o.ProductName,
                                 SkuId = o.ProductSkus.FirstOrDefault().SkuId,
                                 SKU = o.ProductSkus.FirstOrDefault().SKU,
                                 Price = o.ProductSkus.FirstOrDefault().Price
                             });

                var data = await query.ToListAsync();
                return data.AsQueryable();

            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
