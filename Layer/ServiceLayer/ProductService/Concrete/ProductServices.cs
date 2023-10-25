using Dapper;
using DataLayer.Context;
using DataLayer.EntityStock;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using ServiceLayer.Model;
using ServiceLayer.StoreServices;
using System.Data;

namespace ServiceLayer.ProductService.Concrete
{
    public class ProductServices
    {
        private readonly StockContext _context;

        private readonly ProductSKUServices _productSkuServices;

        public ProductServices(StockContext context, ProductSKUServices productskuservices)
        {
            _context = context;
            _productSkuServices = productskuservices;
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
                var ocat = await _context.Products
                   .OrderBy(pc => pc.ProductId)
                   .LastOrDefaultAsync();

                if (ocat != null)
                {
                    return (ocat.ProductId + 1);
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

        public async Task<ProductDto> GetProductAsync(int productId)
        {
            string sql = @"SELECT  products.ProductId, products.ProductName,products.ProductDescription, productskus.SKU
                           FROM  products Inner join  productskus ON products.ProductId = productskus.ProductId where products.ProductId = @productId ;";

            using (IDbConnection connection = new MySqlConnection(_context.Database.GetConnectionString()))
            {
                try
                {
                    var data = await connection.QuerySingleAsync<ProductDto>(sql, new { productId });
                    return data;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
        public ProductDto? GetProduct(int productId)
        {
            try
            {
                // IEnumerable<ProductDto>? products;
                var query = (from cat in _context.Products
                             where cat.ProductId == productId
                             select new ProductDto
                             {
                                 ProductId = cat.ProductId,
                                 CategoryId = cat.CategoryId,
                                 Category = cat.ProductCategory.Category,
                                 ProductName = cat.ProductName,
                                 ProductDescription = cat.ProductDescription,
                                 SKU = cat.ProductSkus.FirstOrDefault().SKU,
                                 Price = cat.ProductSkus.FirstOrDefault().Price
                             });
                var data = query.SingleOrDefault(); // Use SingleOrDefault() here
                return data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<ServiceResponseDTO<bool>> CreateAsync(ProductDto objDTO)
        {
            ServiceResponseDTO<bool> result = new();
            int Id = await GetIDAsync();

            if (Id == -1)
            {
                result.Message = "Tidak bisa simpan data";
                result.Success = false;
                return result;
            }

            objDTO.ProductId = Id;
            try
            {

                var itemToAdd = new Product
                {
                    ProductId = Id,
                    CategoryId = objDTO.CategoryId,
                    ProductName = objDTO.ProductName,
                    ProductDescription = objDTO.ProductDescription,
                    ProductImage = string.Empty
                };

                _context.Add(itemToAdd);

                var affectedRows = await _context.SaveChangesAsync();

                if (affectedRows > 0)
                {
                    // tambahkan SKU 

                    var ItemSkuToAdd = new ProductSKUDto
                    {
                        ProductId = Id,
                        SKU = objDTO.SKU,
                        Price = objDTO.Price
                    };

                    var flg = await _productSkuServices.CreateAsync(ItemSkuToAdd);

                    if (flg.Success == true)
                    {
                        result.Success = true;
                        result.Message = "Data disimpan";
                        return result;
                    }
                    else
                    {
                        // hapus productID juga
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
                result.Message = ex.Message;
                result.Success = false;
                return result;
            }

}

public async Task<IEnumerable<ProductDto>?> GetProductListAsycn()
{
    try
    {
        var query = (from cat in _context.Products
                     select new ProductDto
                     {

                         ProductId = cat.ProductId,
                         CategoryId = cat.CategoryId,
                         Category = cat.ProductCategory.Category,
                         ProductName = cat.ProductName,
                         ProductDescription = cat.ProductDescription,
                         SKU = cat.ProductSkus.FirstOrDefault().SKU,
                         Price = cat.ProductSkus.FirstOrDefault().Price
                     }); 
        var data = await query.ToListAsync();
        return data.AsQueryable();

    }
    catch (Exception ex)
    {
        return null;
    }
}



public async Task<ServiceResponseDTO<bool>> DeleteProductAsync(int ProductId, string productSku)
{
    ServiceResponseDTO<bool> result = new();
    try
    {
        result = await _productSkuServices.DeleteProductSKUAsync(ProductId, productSku);
        if (result.Success == true)
        {
            var productToDelete = await _context.Products.FindAsync(ProductId);
            if (productToDelete != null)
            {
                // Step 2: Remove the entity from the context
                _context.Products.Remove(productToDelete);

                // Step 3: Save the changes to the database

                var affectedRows = await _context.SaveChangesAsync();
                if (affectedRows > 0)
                {
                    result.Success = true;
                    result.Message = "Data dihapus";
                    return result;
                }
                else
                {
                    result.Success = false;
                    result.Message = "Data tidak dihapus";
                    return result;
                }

            }
            else
            {
                result.Success = false;
                result.Message = "Data tidak bisa dihapus";
                return result;
            }
        }
        else
        {
            result.Success = true;
            result.Message = "Data dihapus";
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


public async Task<ServiceResponseDTO<bool>> UpdateAsync(ProductDto objDTO, string oldsku)
{
    ServiceResponseDTO<bool> result = new();
    try
    {
        var prod = await _context.Products.FirstOrDefaultAsync(u => u.ProductId == objDTO.ProductId);

        if (prod != null)
        {
            prod.ProductName = objDTO.ProductName;
            prod.CategoryId = objDTO.CategoryId;
            prod.ProductDescription = objDTO.ProductDescription;

            var affectedRows = await _context.SaveChangesAsync();
            var flg = await _productSkuServices.UpdateAsync(objDTO.ProductId, objDTO.SKU, objDTO.Price, oldsku);

            if (affectedRows > 0)
            {
                result.Success = true;
                result.Message = "Produk berubah . " + flg.Message;
                return result;
            }
            else
            {
                result.Success = false;
                result.Message = "Produk tidak berubah." + flg.Message;  
                return result;
            }
        }
        else
        {
            result.Success = false;
            result.Message = "Data tidak disimpan";
            return result;
        }
    }
    catch (Exception ex)
    {
        result.Success = false;
        result.Message = "Data tidak disimpan";
        return result;
    }
}

    }
}
