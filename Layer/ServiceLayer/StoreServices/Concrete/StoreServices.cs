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
    }
}
