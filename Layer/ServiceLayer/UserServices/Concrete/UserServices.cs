using Common;
using DataLayer.Context;
using DataLayer.EntityStock;
using Microsoft.AspNetCore.Identity;
using ServiceLayer.ResponseServices;
using System.Security.Claims;
using static Common.Identity;

namespace ServiceLayer.UserServices.Concrete
{
    public class UserServices
    {
        private readonly StockContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserLogServices _logger;
        public UserServices(StockContext context, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager, UserLogServices logger)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        public async Task<ServiceResponseDTO<string>> RegisterUser(SignUpRequestDTO objDTO)
        {
            ApplicationUser user;
            ServiceResponseDTO<string> response = new();
            string message = string.Empty;

            var exist = await _userManager.FindByNameAsync(objDTO.UserName);
            if (exist != null)
            {
                response.Success = false;
                response.Message = SD.Register.UserFound;
                response.Data = exist.Id;
                message = "User Name telah ada";
                await createLog(objDTO.UserName, message);

                return response;
            }

            // cek validitas userName
            if (Common.Identity.CheckInputUserName(objDTO.UserName) == PhoneOrEmail.Phone)
            {
                user = new ApplicationUser
                {
                    UserName = objDTO.UserName,
                    Name = objDTO.Name,
                    PhoneNumber = objDTO.UserName,
                    PhoneNumberConfirmed = false,

                };
            }
            else if (CheckInputUserName(objDTO.UserName) == PhoneOrEmail.Email)
            {
                user = new ApplicationUser
                {
                    UserName = objDTO.UserName,
                    Name = objDTO.Name,
                    Email = objDTO.UserName,
                    EmailConfirmed = false
                };
            }
            else
            {   // bentuknya bukan phone atau email

                user = new ApplicationUser
                {
                    UserName = objDTO.UserName,
                    Name = objDTO.Name,
                    EmailConfirmed = false,
                    PhoneNumberConfirmed = false
                };

            }


            var result = await _userManager.CreateAsync(user, objDTO.Password);

            if (!result.Succeeded)
            {


                string errorMessage = string.Empty;
                foreach (var error in result.Errors)
                {
                    errorMessage = error.Description;
                    //  ModelState.AddModelError(string.Empty, error.Description);
                }
                message = "masalah dalam pembuatan user";
                await createLog(objDTO.UserName, message);

                response.Success = false;
                response.Message = errorMessage;
                response.Errors = result.Errors.Select(u => u.Description);
                return (response);
            }

            // Tambahkan claim
            Claim claimPos;
            claimPos = new Claim("Position", objDTO.Role);
            await this._userManager.AddClaimAsync(user, claimPos);

            message = "pembuatan user berhasil";
            await createLog(objDTO.UserName, message);

            response.Data = user.Id;
            response.Success = true;
            response.Message = SD.Register.RegisterSuccess;
            return (response);


        }
        public async Task<ServiceResponseDTO<string>> SignIn(SignInRequest<ApplicationUser> request)
        {
            string userName = request.UserName;
            ServiceResponseDTO<string> response = new ServiceResponseDTO<string>();
            string message = string.Empty;

            //var usr = await _userManager.FindByNameAsync(user.UserName);
            try
            {
                var usr = await _userManager.FindByNameAsync(request.UserName);
                var result = await _signInManager.CheckPasswordSignInAsync(usr, request.Password, true);
                response = new ServiceResponseDTO<string>();

                if (await _signInManager.CanSignInAsync(usr))
                {
                    if (result.Succeeded)
                    {
                        message = SD.Register.SiginSuccess;
                        response.Success = true;
                        response.Message = message;
                        await createLog(userName, message);
                    }
                    else
                    {
                        if (result.IsLockedOut)
                        {
                            message = "Account di lockout dan tidak bisa digunakan sementara ini. Kembali dalam beberapa menit";
                            response.Success = false;
                            response.Message = message;

                            await createLog(userName, message);

                        }
                        else
                        {
                            message = SD.Register.SiginFailed;
                            response.Success = false;
                            response.Message = message;

                            await createLog(userName, message);
                        }
                    }
                    return response;

                }
                else
                {
                    message = SD.Register.SiginFailed;
                    response.Success = false;
                    response.Message = message;

                    await createLog(userName, message);
                    return response;
                }
            }
            catch (Exception ex)
            {
                return new ServiceResponseDTO<string>()
                {
                    Success = false,
                    Message = ex.Message
                };
            }



        }
        public async Task<ServiceResponseDTO<string>> ChangePassword(ApplicationUser user, ChangePasswordDTO objDTO)
        {
            ServiceResponseDTO<string> response = new();


            if (user == null)
            {
                response.Success = false;
                response.Message = SD.Register.UserFound;
                response.Data = user.Id;


                await createLog(user.UserName, response.Message);

                return response;
            }
            else
            {
                var changePasswordResult = await _userManager.ChangePasswordAsync(user, objDTO.OldPassword, objDTO.NewPassword);
                if (!changePasswordResult.Succeeded)
                {
                    response.Success = false;
                    response.Message = SD.Register.UserFound;
                    response.Data = user.Id;


                    await createLog(user.UserName, response.Message);

                    return response;
                }
            }

            response.Success = true;
            response.Message = "Change Password berhasil";
            response.Data = user.Id;

            await createLog(user.UserName, response.Message);

            return response;

        }
        private async Task<bool> createLog(string userId, string message)
        {
            var log = new LoggerDTO
            {
                UserId = userId,
                Timestamp = DateTime.Now,
                message = message

            };

            return await _logger.Create(log);
        }
    }
}
