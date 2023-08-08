using System.Reflection.Metadata.Ecma335;

namespace SimpleEcommerceAspNet6.Models
{
    public class Role_User
    {
        public int RoleId { get; set; }
        public int UserId { get; set; }
        public Role Role { get; set; }
        public User User { get; set; }
    }
}
