using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SimpleEcommerceAspNet6.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Tên Đăng Nhập")]
        [Required(ErrorMessage = "Tên Đăng nhập không được để trống")]
        
        public string UserName { get; set; }

        [Display(Name = "Mật Khẩu")]
        [Required(ErrorMessage = "Mật khẩu không được để trống")]
       
        public string Password { get; set; }
    }
}
