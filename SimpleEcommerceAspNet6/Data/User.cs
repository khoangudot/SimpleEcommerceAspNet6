namespace SimpleEcommerceAspNet6.Data
{
    public class User
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime? Birthday { get; set; }
        public string? Avatar { get; set; }
        public string? Address { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public bool Active { get; set; }
        public int RoleId { get; set; }
        public DateTime? LastLogin { get; set; }
        public DateTime? CreateDate { get; set; }

        public virtual ICollection<Role_User>? Role_Users { get; set; }

    }
}
