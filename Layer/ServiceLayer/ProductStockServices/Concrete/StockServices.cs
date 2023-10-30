using DataLayer.Context;
using DataLayer.EntityStock;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Model;

namespace ServiceLayer.ProductStockServices.Concrete
{
    public class StockServices
    {
        private readonly StockContext _context;
        public StockServices(StockContext context)
        {
            _context = context;
        }
        public async Task<ServiceResponseDTO<bool>> CreateAsync(StockDto objDTO)
        {
            ServiceResponseDTO<bool> result = new();

            try
            {
                var stock = await _context.stocks.FirstOrDefaultAsync(u => u.SkuId == objDTO.SkuId && u.StoreId == objDTO.StoreId);

                if (stock != null)
                {
                    stock.qty += objDTO.qty; // kalau sudah ada tambahkan
                }
                else
                {
                    var ItemToAdd = new Stock
                    {
                        SkuId = objDTO.SkuId,
                        StoreId = objDTO.StoreId,
                        qty = objDTO.qty
                    };
                    _context.Add(ItemToAdd);
                }
                var affectedRows = await _context.SaveChangesAsync();

                result.Success = affectedRows > 0;
                result.Message = result.Success ? "Data Disimpan" : "Data Belum Disimpan";
                return result;


            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Success = false;
                return result;
            }
        }

    }
}
