using System.ComponentModel.DataAnnotations;

namespace ServiceLayer.UserServices
{
    public class SignUpRequestDTO
    {

        public string Name { get; set; }

        [Required(ErrorMessage = "User name Is required")]
        [MinLength (6)]
        public string UserName { get; set; }

        //      [Required(ErrorMessage = "Email is required")]
        //[RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid email address")]

        public string PhoneOrEmail { get; set; }


        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [MinLength (6)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and confirm password is not matched")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Role Is required")]
        public string Role { get; set; }
        public int StoreId { get; set; } // location

    }
}
