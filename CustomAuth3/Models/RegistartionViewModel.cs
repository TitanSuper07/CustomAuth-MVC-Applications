using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CustomAuth3.Models
{
    public class RegistartionViewModel
    {
        [Required(ErrorMessage = "Username is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters")]
        [Display(Name = "Username")]
        [RegularExpression(@"^\S.*$", ErrorMessage = "Cannot contain only whitespace characters")]

        public string UserName { get; set; }

        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [Display(Name = "Email")]
        [RegularExpression(@"^\S.*$", ErrorMessage = "Cannot contain only whitespace characters")]

        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Password { get; set; }

        [NotMapped]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [RegularExpression(@"^\S.*$", ErrorMessage = "Cannot contain only whitespace characters")]

        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Department")]
        [RegularExpression(@"^\S.*$", ErrorMessage = "Cannot contain only whitespace characters")]

        [StringLength(8, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 8 characters")]

        public string Department { get; set; }
    }
}
