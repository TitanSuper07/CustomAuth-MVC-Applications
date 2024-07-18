using System.ComponentModel.DataAnnotations;

namespace CustomAuth3.Models
{
    public class EditUserViewModel
    {
        public int UserId { get; set; }

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

        [Required(ErrorMessage = "Department is required")]
        [StringLength(8, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 8 characters")]

        [Display(Name = "Department")]
        [RegularExpression(@"^\S.*$", ErrorMessage = "Cannot contain only whitespace characters")]

        public string Department { get; set; }

        [Required(ErrorMessage = "Role is required")]
        [Display(Name = "Role")]
        [RegularExpression(@"^\S.*$", ErrorMessage = "Cannot contain only whitespace characters")]

        public int RoleId { get; set; }

        public Role? Role { get; set; }
    }
}
