using CustomAuth3.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CustomAuth3.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required(ErrorMessage = "Username is required")]
        [RegularExpression(@"^\S.*$", ErrorMessage = "Cannot contain only whitespace characters")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters")]
        [Display(Name = "Username")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [Display(Name = "Email")]
        [RegularExpression(@"^\S.*$", ErrorMessage = "Cannot contain only whitespace characters")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [RegularExpression(@"^\S.*$", ErrorMessage = "Cannot contain only whitespace characters")]
        public string Password { get; set; }
        [NotMapped]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [RegularExpression(@"^\S.*$", ErrorMessage = "Cannot contain only whitespace characters")]
        public string ConfirmPassword { get; set; }
        [Required]
        [Display(Name = "Department")]
        [StringLength(6, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 6 characters")]
        [RegularExpression(@"^\S.*$", ErrorMessage = "Cannot contain only whitespace characters")]

        public string Department { get; set; }
        public int RoleId { get; set; }  // Reference to Role        
        public Role? Role { get; set; }

    }

}

//This is a navigation property.It allows us to link the User with many UserRole entries.
//Each user can have multiple roles, and this 
//property will hold all the roles assigned to the user.
//public ICollection<UserRole> UserRoles { get; set; }