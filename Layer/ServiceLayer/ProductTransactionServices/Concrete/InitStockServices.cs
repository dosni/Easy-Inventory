using DataLayer.Context;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Model;
using ServiceLayer.ProductService;
using ServiceLayer.ProductService.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ProductTransactionServices.Concrete
{
    public class InitStockServices
    {
        private readonly StockContext _context;

        public InitStockServices(StockContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Membuat Custom AutoNumber.
        /// Menambah satu kepada setiap record baru
        /// </summary>
        /// <returns></returns>
        public async Task<int> GetIDAsync()
        {

            try
            {
                var ocat = await _context.ProductTransactions
                   .OrderBy(pc => pc.TransactionId)
                   .LastOrDefaultAsync();

                if (ocat != null)
                {
                    return (ocat.TransactionId + 1);
                }
                else
                {
                    return 1;

                }

            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public async Task<ServiceResponseDTO<bool>> CreateAsync(InitialStockDto objDTO)
        {
            ServiceResponseDTO<bool> result = new();
            int Id = await GetIDAsync();

            if (Id == -1)
            {
                result.Message = "Tidak bisa simpan data";
                result.Success = false;
                return result;
            }

            objDTO.TransactionId = Id;

            try
            {
                var ItemToAdd = new InitialStockDto
                {
                    TransactionId = Id,
                    TransactionDate = objDTO.TransactionDate,
                    ProductId = objDTO.ProductId,
                    StoreId = objDTO.StoreId,
                    Price = objDTO.Price,
                    Qty = objDTO.Qty

                };
                _context.Add(ItemToAdd);
                var affectedRows = await _context.SaveChangesAsync();

                if (affectedRows > 0)
                {

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
