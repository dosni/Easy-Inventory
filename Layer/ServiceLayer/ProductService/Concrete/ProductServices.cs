using DataLayer.Context;
using DataLayer.EntityStock;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.ResponseServices;
using ServiceLayer.TransactionServices;
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
                                 UnitId = cat.UnitId,
                                 Unit = cat.ProductUnit.Unit,
                                 ProductName = cat.ProductName,
                                 ProductDescription = cat.ProductDescription,
                                 SkuId = cat.ProductSkus.FirstOrDefault().SkuId,
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
                    UnitId = objDTO.UnitId,
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
                    result.Success = flg.Success;
                    result.Message = flg.Success ? "Data is saved" : "Data is not saved";
                    return result;


                }
                else
                {
                    result.Success = false;
                    result.Message = "Data is not saved";
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
        public async Task<IEnumerable<ProductDisplayDto>?> GetProducPurchasetSelectionAsycn()
        {
            try
            {


                var query = (from p in _context.Products
                             join s in _context.ProductSkus on p.ProductId equals s.ProductId

                             select new ProductDisplayDto
                             {
                                 ProductName = p.ProductName,
                                 SkuId = s.SkuId,
                                 SKU = s.SKU,
                                 Price = s.Price
                             });
                var data = await query.ToListAsync();
                return data.AsQueryable();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<IEnumerable<ProductDisplayDto>?> GetProductSalesSelectionAsycn(int storeId)
        {
            try
            {


                var query = (from p in _context.Products
                             join s in _context.ProductSkus on p.ProductId equals s.ProductId
                             join t in _context.stocks on s.SkuId equals t.SkuId into stockGroup
                             from stock in stockGroup.DefaultIfEmpty() // Equivalent to a RIGHT OUTER JOIN
                             where stock.StoreId == storeId

                             select new ProductDisplayDto
                             {
                                 ProductName = p.ProductName,
                                 SkuId = s.SkuId,
                                 SKU = s.SKU,
                                 Price = s.Price,
                                 StoreId = stock.StoreId,
                                 QtyAvaiable = stock.qty // (stock != null) ? stock.qty : 0 // You might want to handle the case where stock is null
                             });
                var data = await query.ToListAsync();
                return data.AsQueryable();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<IEnumerable<ProductDisplayDto>?> GetProductAdjustmentSelectionAsycn(int storeId)
        {
            try
            {


                /*  
                  First Approach :

                  var query = (from p in _context.Products
                             join s in _context.ProductSkus on p.ProductId equals s.ProductId
                             join t in _context.stocks on s.SkuId equals t.SkuId into stockGroup
                             from stock in stockGroup.DefaultIfEmpty() // Equivalent to a RIGHT OUTER JOIN
                             where stock.StoreId == storeId || stock.StoreId == null

                             select new ProductDisplayDto
                             {
                                 ProductName = p.ProductName,
                                 SkuId = s.SkuId,
                                 SKU = s.SKU,
                                 Price = s.Price,
                                 StoreId = stock.StoreId,
                                 QtyAvaiable = stock.qty
                             });
               
                The performance difference between the two approaches can depend on various factors, including the size of your data, the complexity of your database schema, 
                and how well the database engine can optimize the queries. Let's discuss the general considerations for each approach:

                First Approach (Explicit Joins):
                Pros:

                More explicit control over the join conditions.
                Readable and easy to understand.
                Cons:

                Potential for N+1 query issues: This happens when separate database queries are made for each related entity. For example, if a product has many skus, and each sku has many stocks, 
                you might end up with multiple database queries to fetch the related data.
                
                ------------

                Second Approach (Using Include):
                Pros:

                Eager loading can reduce the number of database queries by fetching related data in a single query.
                More concise code.
                Cons:

                May retrieve more data than needed, especially if you're not careful with filtering in the Include statements.
                Might result in a more complex SQL query generated by the ORM, and the database might have limitations on optimization.

                Considerations:
                Data Size: For small datasets, the performance difference might not be noticeable. For larger datasets, the second approach with eager loading could be more efficient.

                Database Optimization: Modern database engines are often optimized to handle complex queries efficiently. The actual performance might depend on the specific database engine you're using.

                N+1 Issue: If you notice performance issues with the first approach, you can address potential N+1 query problems by using methods like Include or LoadWith selectively.

                Recommendation:
                If your data is relatively small and the first approach is more readable for you, it might be a reasonable choice.

                If you're dealing with larger datasets or notice performance issues, consider using the second approach with Include but be mindful of potential pitfalls.

                In practice, it's often a good idea to profile your application using a tool like Entity Framework Profiler or SQL Server Profiler to analyze the actual queries being executed and 
                their performance characteristics. 
                This can help you make informed decisions about optimizing your queries based on your specific use case.
                   
               */


                // Second Approach

                var query = _context.Products
                    .Include(p => p.ProductSkus)
                    .ThenInclude(s => s.Stocks)
                .Select(p => new ProductDisplayDto
                {
                    ProductName = p.ProductName,
                    SkuId = p.ProductSkus.FirstOrDefault().SkuId,
                    SKU = p.ProductSkus.FirstOrDefault().SKU,
                    StoreId = p.ProductSkus.FirstOrDefault().Stocks.FirstOrDefault(t => t.StoreId == storeId || t.StoreId == null).StoreId,
                    QtyAvaiable = p.ProductSkus.FirstOrDefault().Stocks.FirstOrDefault(t => t.StoreId == storeId || t.StoreId == null).qty,
                    Price = p.ProductSkus.FirstOrDefault().Price
                })
                .Where(dto => dto.StoreId == storeId || dto.StoreId == null);

                var data = await query.ToListAsync();
                return data.AsQueryable();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Do not include product with avaiable quantity.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ProductDisplayDto>?> GetInitialProductSelectionAsycn(int LocationId)
        {

            try
            {
                var productsWithTransactions = await (from s in _context.ProductSkus
                                                      join t in _context.stocks on s.SkuId equals t.SkuId
                                                      where t.StoreId == LocationId
                                                      select s.SkuId).Distinct().ToListAsync();

                // Query to get all products
                var allProductsQuery = (from p in _context.Products
                                        join s in _context.ProductSkus on p.ProductId equals s.ProductId
                                        select new ProductDisplayDto
                                        {
                                            ProductName = p.ProductName,
                                            SkuId = s.SkuId,
                                            SKU = s.SKU,
                                            StoreId = null
                                        });

                var allProducts = await allProductsQuery.ToListAsync();

                // Filter out products with transactions
                var filteredData = allProducts.Where(p => !productsWithTransactions.Contains(p.SkuId));

                return filteredData.AsQueryable();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Mendapatkan Stok dari Lokasi
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ProductDisplayDto>?> GetProductTransferSelectionAsycn(int Id)
        {
            try
            {

                var query = from p in _context.Products
                            join s in _context.ProductSkus on p.ProductId equals s.ProductId
                            join stock in _context.stocks on s.SkuId equals stock.SkuId into stockGroup
                            from stock in stockGroup.DefaultIfEmpty()  // Equivalent to a LEFT OUTER JOIN

                            where stock.StoreId == Id && stock.qty > 0

                            select new ProductDisplayDto
                            {
                                ProductName = p.ProductName,
                                SkuId = s.SkuId,
                                SKU = s.SKU,
                                StoreId = stock.StoreId,
                                QtyAvaiable = stock.qty
                            };
                var data = await query.ToListAsync();
                return data.AsQueryable();
            }
            catch (Exception ex)
            {
                return null;
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

        public async Task<ServiceResponseDTO<bool>> DeleteAsync(int ProductId, string productSku)
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
                        result.Success = affectedRows > 0;
                        result.Message = result.Success ? "Data is deleted" : "Data is not deleted";
                        return result;

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


        public async Task<ServiceResponseDTO<bool>> UpdateAsync(ProductDto objDTO)
        {
            ServiceResponseDTO<bool> result = new();
            try
            {
                var prod = await _context.Products.FirstOrDefaultAsync(u => u.ProductId == objDTO.ProductId);

                if (prod != null)
                {
                    prod.ProductName = objDTO.ProductName;
                    prod.CategoryId = objDTO.CategoryId;
                    prod.UnitId = objDTO.UnitId;
                    prod.ProductDescription = objDTO.ProductDescription;

                    var affectedRows = await _context.SaveChangesAsync();
                    var flg = await _productSkuServices.UpdateAsync(objDTO.SkuId, objDTO.SKU, objDTO.Price);

                    result.Success = affectedRows > 0;
                    result.Message = result.Success ? "Product is changed. " + flg.Message : "Product is not changed. " + flg.Message;
                    return result;


                }
                else
                {
                    result.Success = false;
                    result.Message = "Data is not saved";
                    return result;
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Data is not saved";
                return result;
            }
        }

    }
}
