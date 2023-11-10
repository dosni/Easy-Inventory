using System.ComponentModel.DataAnnotations;
/*
 Digunakan oleh Middleware blazorCookigLoginMiddleware

 */
namespace ServiceLayer.UserServices
{
    public class SignInRequest<TUser> where TUser : class
    {
#nullable disable


        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; }

        //[Required(ErrorMessage = "Please enter Email")]
        //[RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
        // ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public TUser User { get; set; }

        public string TwoFactorCode { get; set; }

        public bool RememberMe { get; set; }

        public bool RememberMachine { get; set; }

        public string ReturnUrl { get; set; } = "/";

        public DateTime LoginStarted { get; set; }

        public string Error { get; set; }
        [Required(ErrorMessage = "Please enter captcha test.")]
        public int Captcha { get; set; }



    }

}

