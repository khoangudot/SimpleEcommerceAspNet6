using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace SimpleEcommerceAspNet6.Models
{
    public class User
    {
        
        public int UserId { get; set; }

        [Display(Name = "Họ và Tên")]
        [Required(ErrorMessage = "Vui lòng nhập Họ Tên")]
        [StringLength(255)]
        public string FullName { get; set; }

        [StringLength(15)]
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        [Display(Name = "Điện thoại")]
        [DataType(DataType.PhoneNumber)]
        [Remote(action: "ValidatePhone", controller: "Uses")]
        public string Phone { get; set; }

        [MaxLength(150)]
        [Required(ErrorMessage = "Vui lòng nhập Email")]
        [EmailAddress]
        [Remote(action: "ValidateEmail", controller: "Uses")]
        public string Email { get; set; }

        public DateTime? Birthday { get; set; }
        public string? Avatar { get; set; }
        public string? Address { get; set; }

        [Required(ErrorMessage ="Vui lòng nhập username")]
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
