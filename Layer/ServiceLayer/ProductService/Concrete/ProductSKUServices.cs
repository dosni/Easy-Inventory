using DataLayer.Context;
using DataLayer.EntityStock;
using ServiceLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


    }
}
