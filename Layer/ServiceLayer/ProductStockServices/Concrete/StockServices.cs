using DataLayer.Context;
using DataLayer.EntityStock;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Model;
using ServiceLayer.ProductTransactionServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var stock = await _context.stocks.FirstOrDefaultAsync(u => u.SkuId == objDTO.SkuId && u.StoreId == objDTO.StoreId);
            if (stock != null)
            {
                result.Success = false;
                result.Message = "Data sudah ada";
                return result;
            }
            try
            {
                var ItemToAdd = new Stock
                { SkuId = objDTO.SkuId,
                  StoreId = objDTO.StoreId,
                  qty = objDTO.qty
                };
                _context.Add(ItemToAdd);
                var affectedRows = await _context.SaveChangesAsync();

                if (affectedRows > 0)
                {
                    // update stock
                    result.Success = true;
                    result.Message = "Data disimpan";
                    return result;
                }
                else
                {

                    result.Success = false;
                    result.Message = "Data Belum disimpan";
                    return result;
                }
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
