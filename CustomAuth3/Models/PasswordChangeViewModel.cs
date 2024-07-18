using System.ComponentModel.DataAnnotations;

namespace CustomAuth3.Models
{
    public class PasswordChangeViewModel
    {
        public int UserId { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current Password")]
        [RegularExpression(@"^\S.*$", ErrorMessage = "Cannot contain only whitespace characters")]

        public string CurrentPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        [RegularExpression(@"^\S.*$", ErrorMessage = "Cannot contain only whitespace characters")]

        public string NewPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [RegularExpression(@"^\S.*$", ErrorMessage = "Cannot contain only whitespace characters")]

        [Compare("NewPassword", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

    }
}
