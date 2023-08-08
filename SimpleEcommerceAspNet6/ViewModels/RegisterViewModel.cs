using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SimpleEcommerceAspNet6.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "Họ và Tên")]
        [Required(ErrorMessage = "Vui lòng nhập Họ Tên")]
        [StringLength(255)]
        public string FullName { get; set; }

        [StringLength(15)]
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        [Display(Name = "Điện thoại")]
        [DataType(DataType.PhoneNumber)]
        [Remote(action: "ValidatePhone", controller: "Users")]
        public string Phone { get; set; }

        [Display(Name = "Email")]
        [MaxLength(150)]
        [Required(ErrorMessage = "Vui lòng nhập Email")]
        [EmailAddress]
        [Remote(action: "ValidateEmail", controller: "Users")]
        public string Email { get; set; }

        [Display(Name = "Tên Đăng Nhập")]
        [Required(ErrorMessage ="Tên Đăng nhập không được để trống")]
        [StringLength(50, MinimumLength = 3)]
        public string UserName { get; set; }


        [Display(Name = "Mật Khẩu")]
        [Required(ErrorMessage ="Mật khẩu không được để trống")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,50}$",
         ErrorMessage = "Mật khẩu phải chứa ít nhất 1 ký tự số, 1 ký tự đặc biệt, 1 ký tự viết hoa và 1 ký tự viết thường.")]
        public string Password { get; set; }

        [Display(Name = "Nhập Lại Mật Khẩu")]
        [Required(ErrorMessage = "Nhập lại mật khẩu không đúng")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,50}$",
        ErrorMessage = "Mật khẩu phải chứa ít nhất 1 ký tự số, 1 ký tự đặc biệt, 1 ký tự viết hoa và 1 ký tự viết thường.")]
        [Compare("Password", ErrorMessage = "Nhập lại mật khẩu không đúng")]
        public string ConfirmPassword { get; set; }
    }
}
