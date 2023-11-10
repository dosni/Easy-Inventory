using System.Text.RegularExpressions;

namespace Common
{
    public class Identity
    {
        public enum PhoneOrEmail
        {
            Email,
            Phone,
            Error
        }
        public static bool IsValidEmailOrPhone(string userName)
        {

            string regexPhone = @"^\+?\d{1,3}[- ]?\d{3,5}[- ]?\d{4}$";
            string regexEmail = "^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$"; // Regex pattern for email address
            bool flg;

            /*
            [RegularExpression(@"^\+?\d{1,3}[- ]?\d{3,5}[- ]?\d{4}$", ErrorMessage = "Invalid phone number")]
             [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid email address")]

             */

            if (Regex.IsMatch(userName, regexPhone))
            {
                flg = true;
            }
            else if (Regex.IsMatch(userName, regexEmail))
            {
                flg = true;
            }
            else
            {
                flg = false;
            }
            return flg;
        }

        public static PhoneOrEmail CheckInputUserName(string userName)
        {

            string regexPhone = @"^\+?\d{1,3}[- ]?\d{3,5}[- ]?\d{4}$";
            string regexEmail = "^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$"; // Regex pattern for email address


            if (Regex.IsMatch(userName, regexPhone))
            {
                return PhoneOrEmail.Phone;
            }

            return PhoneOrEmail.Email;
        }
    }
}
