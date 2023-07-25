using System.Security.Principal;

namespace SimpleEcommerceAspNet6.Data
{
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<Role_User> Role_Users { get; set; }
    }
}
