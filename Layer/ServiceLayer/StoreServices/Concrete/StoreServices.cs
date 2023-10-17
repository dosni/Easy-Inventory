using DataLayer.Context;
using DataLayer.EntityStock;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Model;


namespace ServiceLayer.StoreServices.Concrete
{
    public class StoreServices
    {
        private readonly StockContext _context;
        public StoreServices(StockContext context)
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

                var ost = await _context.Stores
                   .OrderBy(st => st.StoreId)
                   .LastOrDefaultAsync();

                if (ost != null)
                {
                    return (ost.StoreId + 1);
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


        public async Task<ServiceResponseDTO<bool>> CreateAsync(StoreDto objDTO)
        {
            ServiceResponseDTO<bool> result = new();
            int Id = await GetIDAsync();

            if (Id == -1)
            {
                result.Message = "Tidak bisa simpan data";
                result.Success = false;
                return result;
            }

            objDTO.StoreId = Id;


            try
            {
                var itemToAdd = new Store
                {
                    StoreId = objDTO.StoreId,
                    StoreName = objDTO.StoreName
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

        public async Task<IEnumerable<StoreDto>?> GetStoreListAsycn()
        {
            try
            {
                var query = (from store in _context.Stores
                             select new StoreDto
                             {

                                 StoreId = store.StoreId,
                                 StoreName = store.StoreName

                             });
                var data = await query.ToListAsync();
                return data.AsQueryable();

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ServiceResponseDTO<bool>> DeleteLocationAsync(int StoreId)
        {
            ServiceResponseDTO<bool> result = new();
            try
            {
                // Step 1: Retrieve the entity you want to delete
                var categoryToDelete = await _context.Stores.FindAsync(StoreId);
                if (categoryToDelete != null)
                {
                    // Step 2: Remove the entity from the context
                     _context.Stores.Remove(categoryToDelete);

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
        public async Task<ServiceResponseDTO<bool>> UpdateAsync(StoreDto objDTO)
        {
            ServiceResponseDTO<bool> result = new();
            try
            {
                var st = await _context.Stores.FirstOrDefaultAsync(u => u.StoreId == objDTO.StoreId);

                if (st != null)
                {
                    st.StoreName= objDTO.StoreName;
                    var affectedRows = await _context.SaveChangesAsync();
                    if (affectedRows > 0)
                    {
                        result.Success = true;
                        result.Message = "Data Dismpan";
                        return result;
                    }
                    else
                    {
                        result.Success = false;
                        result.Message = "Data tidak disimpan";
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
