using System.ComponentModel.DataAnnotations;

namespace SimpleEcommerceAspNet6.Data
{
    public class User
    {
        
        public int UserId { get; set; }

        [Required]
        [StringLength(255)]
        public string FullName { get; set; }

        [Required]
        [StringLength(15)]
        public string Phone { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public DateTime? Birthday { get; set; }
        public string? Avatar { get; set; }
        public string? Address { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string UserName { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,50}$",
         ErrorMessage = "Mật khẩu phải chứa ít nhất 1 ký tự số, 1 ký tự đặc biệt, 1 ký tự viết hoa và 1 ký tự viết thường.")]
        public string Password { get; set; }
        public string? Salt { get; set; }
        public bool Active { get; set; }
        public DateTime? LastLogin { get; set; }
        public DateTime? CreateDate { get; set; }
        public virtual ICollection<Role_User>? Role_Users { get; set; }
        public virtual Customer? Customer { get; set; }
    }
}
