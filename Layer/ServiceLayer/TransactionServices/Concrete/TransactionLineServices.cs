using Common;
using DataLayer.Context;
using DataLayer.EntityStock;
using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI.Common;
using ServiceLayer.Model;
using ServiceLayer.ProductService;
using ServiceLayer.ProductStockServices;
using ServiceLayer.ProductStockServices.Concrete;

namespace ServiceLayer.TransactionServices.Concrete
{
    public class TransactionLineServices
    {
        private readonly StockContext _context;
        private readonly StockServices _stockServices;
        public TransactionLineServices(StockContext context, StockServices stockServices)
        {
            _context = context;
            _stockServices = stockServices;
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
                var ocat = await _context.TransactionLines
                   .OrderBy(pc => pc.LineId)
                   .LastOrDefaultAsync();

                if (ocat != null)
                {
                    return (ocat.LineId + 1);
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
        private async Task<ServiceResponseDTO<bool>> UpdateStock(TransactionLineDto lineDto)
        {
            ServiceResponseDTO<bool> result = new();
            try
            {

                var StockToAdd = new StockDto
                {
                    StoreId = lineDto.StoreId,
                    SkuId = lineDto.SkuId,
                    qty = lineDto.Qty
                };
                result = await _stockServices.CreateAsync(StockToAdd);

                return result;

            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Success = false;
                return result;
            }
        }
      
        public async Task<ServiceResponseDTO<bool>> AddPurchaseAsync(int transactionId, IEnumerable<TransactionLineDto> linesDto)
        {
            ServiceResponseDTO<bool> result = new();

            try
            {
               

                foreach (var data in linesDto)
                {
                    // Kalau transaksinya adalah pengembalian barang maka jumlah negatif
                    if(data.TransactionType == ((int)TransactionType.Purchase_Return).ToString())
                    {
                        data.Qty = - (data.Qty);
                    }
                    var ItemToAdd = new TransactionLine
                    {
                        TransactionId = transactionId,
                        LineId = await GetIDAsync(),
                        TransactionType = data.TransactionType,
                        StoreId = data.StoreId,
                        SkuId = data.SkuId,
                        Qty = data.Qty,
                        Price = data.Price
                    };
                    _context.Add(ItemToAdd);
                    var affectedRows = await _context.SaveChangesAsync();
                    if (affectedRows > 0)
                    {
                        await UpdateStock(data);
                    }
      
                }

                result.Success = true;
                result.Message = result.Success ? "Data saved" : "Data is not saved";
                return result;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Success = false;
                return result;
            }
        }
        public async Task<ServiceResponseDTO<bool>> AddInitialStockAsync(int transactionId, IEnumerable<TransactionLineDto> linesDto)
        {
            ServiceResponseDTO<bool> result = new();

            try
            {
                // Tambahkan data produk ke TransactionLines

                foreach (var data in linesDto)
                {
                    var tran = await _context.TransactionLines.FirstOrDefaultAsync(u => u.SkuId == data.SkuId && u.StoreId == data.StoreId);
                    if (tran == null)
                    {
                        var ItemToAdd = new TransactionLine
                        {
                            TransactionId = transactionId,
                            LineId = await GetIDAsync(),
                            TransactionType = data.TransactionType,
                            StoreId = data.StoreId,
                            SkuId = data.SkuId,
                            Qty = data.Qty,
                            Price = data.Price
                        };
                        _context.Add(ItemToAdd);
                        var affectedRows = await _context.SaveChangesAsync();
                        if (affectedRows > 0)
                        {
                            await UpdateStock(data); 
                        }
                    }
                    else
                    {
                        result.Success = false;
                        result.Message = result.Success ? "Data saved" : "Some or all data is not saved";
                        return result;
                    }
                }

                result.Success = true;
                result.Message = result.Success ? "Data saved" : "Data is not saved";
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
