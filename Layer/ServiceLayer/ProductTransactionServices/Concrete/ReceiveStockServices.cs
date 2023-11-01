using DataLayer.Context;
using DataLayer.EntityStock;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Model;
using ServiceLayer.ProductService.Concrete;
using ServiceLayer.ProductStockServices;
using ServiceLayer.ProductStockServices.Concrete;

namespace ServiceLayer.ProductTransactionServices.Concrete
{
    public class ReceiveStockServices
    {
        private readonly StockContext _context;
        private readonly StockServices _stockServices;

        public ReceiveStockServices(StockContext context, StockServices StockServices)
        {
            _context = context;
            _stockServices = StockServices;
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
                var ocat = await _context.Transactions
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

        /// <summary>
        /// Kalau sudah ada transaksi stok awal, tidak boleh membuatnya lagi.
        /// Tetapi kalau untuk pembelian bisa berulang ulang
        /// </summary>
        /// <param name="objDTO"></param>
        /// <returns></returns>
        public async Task<ServiceResponseDTO<bool>> CreateAsync(ProductTransactionDto objDTO)
        {
            ServiceResponseDTO<bool> result = new();

            if (objDTO.TransactionType == "I")
            {
                var tran = await _context.TransactionLines.FirstOrDefaultAsync(u => u.SkuId == objDTO.SkuId && u.StoreId == objDTO.StoreId);
                if (tran != null)
                {
                    result.Success = false;
                    result.Message = "Data sudah ada";
                    return result;
                }
            }




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
                var ItemToAdd = new Transaction
                {
                    TransactionId = Id,
                    TransactionDate = objDTO.TransactionDate ?? DateTime.Now, // Converting from non Nullable to nullable . if null assgin to datetime.Now
                    TransactionType = objDTO.TransactionType,
                    //SkuId = objDTO.SkuId,
                    //StoreId = objDTO.StoreId,
                    //Price = objDTO.Price,
                    //Qty = objDTO.Qty,
                    CreatedAt = objDTO.CreatedAt,
                    CreatedBy = objDTO.CreatedBy,
                    Description = objDTO.Description,
                    IsPosted = true
                };
                _context.Add(ItemToAdd);
                var affectedRows = await _context.SaveChangesAsync();

                if (affectedRows > 0)
                {
                    // Add stock
                    var StockToAdd = new StockDto
                    {
                        StoreId = objDTO.StoreId,
                        SkuId = objDTO.SkuId,
                        qty = objDTO.Qty
                    };

                    var flg = await _stockServices.CreateAsync(StockToAdd);



                    result.Success = flg.Success;
                    result.Message = flg.Success ? "Data Disimpan" : "Data Belum Disimpan";
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
