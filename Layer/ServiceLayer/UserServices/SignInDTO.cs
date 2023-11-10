using System.ComponentModel.DataAnnotations;

namespace ServiceLayer.UserServices
{
    public class SignInDTO
    {
        [Required(ErrorMessage = "UserName is required")]

        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
