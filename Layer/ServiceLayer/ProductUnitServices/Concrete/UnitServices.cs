using DataLayer.Context;
using DataLayer.EntityStock;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.ResponseServices;

namespace ServiceLayer.ProductUnitServices.Concrete
{
    public class UnitServices
    {
        private readonly StockContext _context;

        public UnitServices(StockContext context)
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
                var ocat = await _context.ProductUnits
                   .OrderBy(pc => pc.UnitId)
                   .LastOrDefaultAsync();

                if (ocat != null)
                {
                    return (ocat.UnitId + 1);
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
        public async Task<int> GetUnitId(string unit)
        {
            try
            {
                var cat = await _context.ProductUnits.FirstOrDefaultAsync(u => u.Unit== unit);
                if (cat != null)
                {

                    return cat.UnitId;
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
        public async Task<ServiceResponseDTO<bool>> CreateAsync(UnitDto objDTO)
        {
            ServiceResponseDTO<bool> result = new();
            int Id = await GetIDAsync();

            if (Id == -1)
            {
                result.Message = "Tidak bisa simpan data";
                result.Success = false;
                return result;
            }

            objDTO.UnitId = Id;

            try
            {
                var itemToAdd = new ProductUnit
                {
                    UnitId = objDTO.UnitId,
                    Unit = objDTO.Unit
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

        public async Task<ServiceResponseDTO<bool>> UpdateAsync(UnitDto objDTO)
        {
            ServiceResponseDTO<bool> result = new();
            try
            {
                var cat = await _context.ProductUnits.FirstOrDefaultAsync(u => u.UnitId == objDTO.UnitId);
                if (cat != null)
                {
                    cat.Unit = objDTO.Unit;

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


        public async Task<IEnumerable<UnitDto>?> GetUnitListAsycn()
        {
            try
            {
                var query = (from cat in _context.ProductUnits
                             select new UnitDto
                             {
                                 UnitId = cat.UnitId,
                                 Unit = cat.Unit
                             });
                var data = await query.ToListAsync();
                return data.AsQueryable();

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ServiceResponseDTO<bool>> DeleteUnitAsync(int unitId)
        {
            ServiceResponseDTO<bool> result = new();
            try
            {
                // Step 1: Retrieve the entity you want to delete
                var unitToDelete = await _context.ProductUnits.FindAsync(unitId);

                if (unitToDelete != null)
                {
                    // Step 2: Remove the entity from the context
                    _context.ProductUnits.Remove(unitToDelete);

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
