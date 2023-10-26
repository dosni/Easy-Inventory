using Dapper;
using DataLayer.Context;
using DataLayer.EntityStock;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using ServiceLayer.Model;
using System.Data;

namespace ServiceLayer.ProductCategoryServices.Concrete
{
    public class CategoryServices
    {
        private readonly StockContext _context;

        public CategoryServices(StockContext context)
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
                var ocat = await _context.ProductCategories
                   .OrderBy(pc => pc.CategoryId)
                   .LastOrDefaultAsync();

                if (ocat != null)
                {
                    return (ocat.CategoryId + 1);
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
        /// Count Number of records
        /// </summary>
        /// <returns></returns>
        private int Count()
        {

            try
            {
                using (IDbConnection connection = new MySqlConnection(_context.Database.GetConnectionString()))
                {

                    try
                    {
                        var sql = "SELECT COUNT(*) FROM productcategories;";
                        var count = connection.ExecuteScalar(sql);

                        // count adalah long > convert to int32
                        int i = Convert.ToInt32(count);

                        return i;

                    }
                    catch (Exception ex)
                    {
                        string msg = ex.Message;
                        return 0;
                    }

                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return 0;
            }

        }
        public async Task<int> GetCategoryId(string category)
        {
            try
            {
                var cat = await _context.ProductCategories.FirstOrDefaultAsync(u => u.Category == category);
                if (cat != null)
                {

                    return cat.CategoryId;
                }
                else
                {
                    return -1;
                }

            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        public async Task<ServiceResponseDTO<bool>> UpdateAsync(CategoryDto objDTO)
        {
            ServiceResponseDTO<bool> result = new();
            try
            {
                var cat = await _context.ProductCategories.FirstOrDefaultAsync(u => u.CategoryId == objDTO.CategoryId);
                if (cat != null)
                {
                    cat.Category = objDTO.Category;

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

        public async Task<ServiceResponseDTO<bool>> CreateAsync(CategoryDto objDTO)
        {
            ServiceResponseDTO<bool> result = new();
            int custId = await GetIDAsync();

            if (custId == -1)
            {
                result.Message = "Tidak bisa simpan data";
                result.Success = false;
                return result;
            }

            objDTO.CategoryId = custId;

            try
            {
                var itemToAdd = new ProductCategory
                {
                    CategoryId = objDTO.CategoryId,
                    Category = objDTO.Category
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
                result.Message = ex.Message;
                result.Success = false;
                return result;
            }

        }

        public async Task<IEnumerable<CategoryDto>> GetCategoryListAsycn()
        {
            string sql = @"select CategoryId, Category from  ProductCategories;";
            try
            {
                using (IDbConnection connection = new SqlConnection(_context.Database.GetConnectionString()))
                {
                    var data = await connection.QueryAsync<CategoryDto>(sql, new { });
                    return data.ToList().AsQueryable();
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return null;
            }

        }
        public async Task<IEnumerable<CategoryDto>?> GetEFCategoryListAsycn()
        {
            try
            {
                var query = (from cat in _context.ProductCategories
                             select new CategoryDto
                             {
                                 CategoryId = cat.CategoryId,
                                 Category = cat.Category
                             });
                var data = await query.ToListAsync();
                return data.AsQueryable();

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ServiceResponseDTO<bool>> DeleteCategoryAsync(int categoryId)
        {
            ServiceResponseDTO<bool> result = new();
            try
            {
                // Step 1: Retrieve the entity you want to delete
                var categoryToDelete = await _context.ProductCategories.FindAsync(categoryId);

                if (categoryToDelete != null)
                {
                    // Step 2: Remove the entity from the context
                    _context.ProductCategories.Remove(categoryToDelete);

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
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                return result;
            }

        }
    }
}
