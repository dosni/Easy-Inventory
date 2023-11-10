using Dapper;
using DataLayer.Context;
using DataLayer.EntityStock;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using ServiceLayer.ResponseServices;
using ServiceLayer.StoreServices;
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

        public async Task<IEnumerable<ProductListDto>?> GetProductNameListAsycn()
        {
            try
            {
                var query = (from cat in _context.Products
                             select new ProductListDto
                             {
                                 ProductId = cat.ProductId,
                                 ProductName = cat.ProductName
                             });
                var data = await query.ToListAsync();
                return data.AsQueryable();
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return null;
            }
        }
        public async Task<IEnumerable<ProductDisplayDto>?> GetProductSelectionAsycn()
        {
            try
            {
                var query = (from o in _context.Products
                             select new ProductDisplayDto
                             {
                                 ProductName = o.ProductName,
                                 SkuId = o.ProductSkus.FirstOrDefault().SkuId,
                                 SKU = o.ProductSkus.FirstOrDefault().SKU,
                                 QtyAvaiable=o.ProductSkus.FirstOrDefault().Stocks.FirstOrDefault().qty,
                                 Price = o.ProductSkus.FirstOrDefault().Price
                             });

                var data = await query.ToListAsync();
                return data.AsQueryable();

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
                //var query = (from o in _context.Products where (o.ProductSkus.First().Stocks.First().Store.StoreId == Id)
                //             select new ProductDisplayDto
                //             {
                //                 ProductName = o.ProductName,
                //                 SkuId = o.ProductSkus.First().SkuId,
                //                 SKU = o.ProductSkus.First().SKU,
                //                 QtyAvaiable = o.ProductSkus.First().Stocks.First().qty
                //             });



                // where loan.Stsrec == "A"
                var query =(from p in _context.Products 
                            join s in _context.ProductSkus on p.ProductId equals s.ProductId 
                            join t in _context.stocks on s.SkuId equals t.SkuId
                            where t.StoreId == Id
                            select new ProductDisplayDto
                              {
                                  ProductName = p.ProductName,
                                  SkuId = s.SkuId,
                                  SKU = s.SKU,
                                  QtyAvaiable = t.qty
                              });
                var data = await query.ToListAsync();
                return data.AsQueryable();

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //var query = (
        //          from loan in _context.MLoans
        //          join cif in _context.MCifs on loan.Nocif equals cif.Nocif
        //          join kycp in _context.Mkycps on cif.Nocif equals kycp.Nocif
        //          join ao in _context.Marketings on loan.Kdaoh equals ao.Kodeao
        //          join tabungan in _context.MTabungancs on loan.Noacdroping equals tabungan.Noacc into tabunganGroup
        //          from tabungan in tabunganGroup.DefaultIfEmpty()


        //          where loan.Stsrec == "A"

        //          orderby loan.Tgljtempo
        //          select new LoanAODto
        //          {
        //              noacc = loan.Noacc,
        //              fnama = loan.Fnama,
        //              dnd = loan.Dnd,
        //              kecamatan = kycp.Kecamatan,
        //              kelurahan = kycp.Kelurahan,
        //              nama_ao = ao.Ket,
        //              kdkolektor = loan.Kdkolektor,
        //              tahunbuka = loan.Tglbuka.Substring(0, 4)
        //          }
        //      );
        //var resultList = await query.ToListAsync();

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
