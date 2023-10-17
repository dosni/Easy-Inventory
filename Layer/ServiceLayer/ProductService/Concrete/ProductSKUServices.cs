using DataLayer.Context;
using DataLayer.EntityStock;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Model;

namespace ServiceLayer.ProductService.Concrete
{
    public class ProductSKUServices
    {
        private readonly StockContext _context;
        public ProductSKUServices(StockContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponseDTO<bool>> CreateAsync(ProductSKUDto objDTO)
        {
            ServiceResponseDTO<bool> result = new();
            try
            {
                var itemToAdd = new ProductSku
                {
                    ProductId = objDTO.ProductId,
                    SKU = objDTO.SKU,
                    Price = objDTO.Price
                };

                _context.Add(itemToAdd);
                var affectedRows = await _context.SaveChangesAsync();

                if (affectedRows > 0)
                {
                    // The operation was successful, and data was saved to the database.
                    // You can put your success handling code here.

                    result.Success = true;
                    result.Message = "Data disimpan";
                    return result;
                }
                else
                {
                    // The operation was not successful, and no data was saved to the database.
                    // You can handle the failure here, possibly by throwing an exception or logging an error.

                    result.Success = false;
                    result.Message = "Data belum disimpan";
                    return result;
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Data belum disimpan";
                return result;
            }

        }

        /// <summary>
        /// Count Number of records
        /// </summary>
        /// <returns></returns>
        private int Count(int Id)
        {

            try
            {
                int count = _context.ProductSkus.Count(u => u.ProductId == Id);

                try
                {
                    return count;
                }
                catch (Exception ex)
                {
                    string msg = ex.Message;
                    return 0;
                }


            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return 0;
            }

        }
        public async Task<ServiceResponseDTO<bool>> UpdateAsync(int ProductId, string productSku, decimal price, string oldsku)
        {

            ServiceResponseDTO<bool> result = new();
            try
            {
                // Untuk update pada key value harus di hapus
                var flg = await DeleteProductSKUAsync(ProductId, oldsku);

                if (flg.Success)
                {
                    var itemToAdd = new ProductSku
                    {
                        ProductId = ProductId,
                        SKU = productSku,
                        Price = price
                    };

                    _context.Add(itemToAdd);
                    var affectedRows = await _context.SaveChangesAsync();

                    if (affectedRows > 0)
                    {
                        // The operation was successful, and data was saved to the database.
                        // You can put your success handling code here.

                        result.Success = true;
                        result.Message = "Data disimpan";
                        return result;
                    }
                    else
                    {
                        // The operation was not successful, and no data was saved to the database.
                        // You can handle the failure here, possibly by throwing an exception or logging an error.

                        result.Success = false;
                        result.Message = "Data belum disimpan";
                        return result;
                    }

                }
                else
                {
                    result.Success = false;
                    result.Message = "Data belum disimpan";
                    return result;
                }
          
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                return result;
            }

        }



        /// <summary>
        /// Hapus ProductSKU. Kalau productId dari ProductSKU setelah dihapus masih ada productId di tabel Products
        /// tidak boleh dihapus sehingga return = false
        /// tetapi kalau productSKU hanya satu maka return = true yang artinya productId di products itu juga harus dihapus
        /// </summary>
        /// <param name="ProductId"></param>
        /// <param name="productSku"></param>
        /// <returns></returns>
        public async Task<ServiceResponseDTO<bool>> DeleteProductSKUAsync(int ProductId, string productSku)
        {
            ServiceResponseDTO<bool> result = new();
            try
            {
                // Step 1: Retrieve the entity you want to delete
                var producskuToDelete = await _context.ProductSkus.FirstOrDefaultAsync(u => u.ProductId == ProductId && u.SKU == productSku);

                if (producskuToDelete != null)
                {


                    // Step 2: Remove the entity from the context
                    _context.ProductSkus.Remove(producskuToDelete);
                    // Step 3: Save the changes to the database

                    var affectedRows = await _context.SaveChangesAsync();

                    if (affectedRows > 0)
                    {
                        int count = Count(ProductId);
                        // berapa jumlah productId setelah dihapus ? Kalau masih ada return false
                        if (count > 0)
                        {
                            result.Success = false;
                            result.Message = "Delete Gagal";
                            return result;
                        }
                        else
                        {
                            // ProductId sudah habis jadi product bisa dihapus
                            result.Success = true;
                            result.Message = "Delete Berhasil";
                            return result;
                        }

                    }
                    else
                    {
                        result.Success = false;
                        result.Message = "Delete Gagal";
                        return result;
                    }
                }
                else
                {
                    result.Success = false;
                    result.Message = "Delete Gagal";
                    return result;
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                return result;
            }

        }

    }
}
