using System.ComponentModel.DataAnnotations;

namespace CustomAuth3.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username is required")]
        [RegularExpression(@"^\S.*$", ErrorMessage = "Cannot contain only whitespace characters")]

        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^\S.*$", ErrorMessage = "Cannot contain only whitespace characters")]

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
