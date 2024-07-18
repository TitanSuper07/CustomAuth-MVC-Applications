using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CustomAuth3.Models
{
    public class Role
    {
        public int RoleId { get; set; }
        public string Name { get; set; }


        //This is a navigation property.It allows us to link the Role with many UserRole
        //entries.Each role can be assigned to multiple users, and this property will hold
        //all the users assigned to the role
        public ICollection<User> User { get; set; }
    }
}
