using DataLayer.Context;
using DataLayer.EntityStock;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Model;
using ServiceLayer.ProductCategoryServices;

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
                // tambahkan SKU dahulu

                var ItemSkuToAdd = new ProductSKUDto
                {
                    ProductId = Id,
                    SKU = objDTO.SKU,
                    Price = objDTO.Price
                };
                var flg = await _productSkuServices.CreateAsync(ItemSkuToAdd);


                // kalau Product SKU bisa disimpan maka simpan product
                // 
                if (flg.Success == true)
                {
                    // The operation was successful, and data was saved to the database.
                    // You can put your success handling code here.
                    var itemToAdd = new Product
                    {
                        ProductId = Id,
                        CategoryId = objDTO.CategoryId,
                        ProductName = objDTO.ProductName,
                        ProductDescription = objDTO.ProductDescription,
                        ProductImage = string.Empty

                    };
                    _context.Add(itemToAdd);


                    await _context.SaveChangesAsync();
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
                                 SKU = cat.ProductSku.SKU,
                                 Price = cat.ProductSku.Price
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
