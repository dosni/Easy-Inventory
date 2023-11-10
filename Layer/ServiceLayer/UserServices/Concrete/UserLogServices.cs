using DataLayer.Context;
using DataLayer.EntityStock;
using ServiceLayer.Model;

namespace ServiceLayer.UserServices.Concrete
{

    public class UserLogServices
    {
        private readonly StockContext _context;
        public UserLogServices(StockContext context)
        {
            _context = context;
        }
        public async Task<bool> Create(LoggerDTO objDTO)
        {
            ServiceResponseDTO<string> result = new();

            try
            {
                var itemToAdd = new ApplicationActivity
                {
                    message = objDTO.message,
                    Timestamp = DateTime.Now,
                    UserId = objDTO.UserId
                };
                _context.Add(itemToAdd);


                await _context.SaveChangesAsync();
                result.Success = true;
                result.Message = "Data disimpan";
                return result.Success;
            }
            catch (Exception ex)
            {
                return false;
            }


        }

    }
}
