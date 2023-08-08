using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using AspNetCoreHero.ToastNotification.Notyf;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SimpleEcommerceAspNet6.Data;
using SimpleEcommerceAspNet6.Extension;
using SimpleEcommerceAspNet6.Helpper;
using SimpleEcommerceAspNet6.Models;
using SimpleEcommerceAspNet6.ViewModels;

namespace SimpleEcommerceAspNet6.Controllers
{
    public class UsersController : Controller
    {
        private readonly EcommerceDbContext _context;
        private readonly INotyfService _notyfService;

        public UsersController(EcommerceDbContext context, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ValidatePhone(string Phone)
        {
            try
            {
                var khachhang = _context.Users.AsNoTracking().SingleOrDefault(x => x.Phone.ToLower() == Phone.ToLower());
                if (khachhang != null)
                    return Json(data: "Số điện thoại : " + Phone + "đã được sử dụng");

                return Json(data: true);

            }
            catch
            {
                return Json(data: true);
            }
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ValidateEmail(string Email)
        {
            try
            {
                var khachhang = _context.Users.AsNoTracking().SingleOrDefault(x => x.Email.ToLower() == Email.ToLower());
                if (khachhang != null)
                    return Json(data: "Email : " + Email + " đã được sử dụng");
                return Json(data: true);
            }
            catch
            {
                return Json(data: true);
            }
        }
        

        // GET: Users/Register
        [AllowAnonymous]
        [Route("register.html", Name = "Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("register.html", Name = "Register")]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string salt = Utilities.GetRandomKey();
                    User user = new User
                    {
                        FullName = registerViewModel.FullName,
                        Phone = registerViewModel.Phone.Trim().ToLower(),
                        Email = registerViewModel.Email.Trim().ToLower(),
                        UserName = registerViewModel.UserName,
                        Password = (registerViewModel.Password + salt.Trim()).ToMD5(),
                        Active = true,
                        Salt = salt,
                        CreateDate = DateTime.Now
                        
                    };
                    try
                    {
                       
                        _context.Add(user);
                        await _context.SaveChangesAsync();
                        
                        _notyfService.Success("Đăng ký thành công");
                        return RedirectToAction("Login", "Users");
                    }
                    catch
                    {
                        return RedirectToAction("Register", "Users");
                    }
                }
                else
                {
                    return View(registerViewModel);
                }
            }
            catch
            {
                return View(registerViewModel);
            }
        }

        [AllowAnonymous]
        [Route("login.html", Name = "Login")]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("login.html", Name = "Login")]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                  
                    var user = _context.Users.AsNoTracking().SingleOrDefault(x => x.UserName.Trim() == loginViewModel.UserName);
                  
                    if (user == null) 
                    {
                        _notyfService.Error("Tên đăng nhập không tồn tại");
                        return View(loginViewModel);
                    } 
                    string passWord = (loginViewModel.Password + user.Salt.Trim()).ToMD5();
                    if (user.Password != passWord)
                    {
                        _notyfService.Error("Thông tin đăng nhập chưa chính xác");
                        return View(loginViewModel);
                    }
                    //kiem tra xem account co bi disable hay khong

                    if (user.Active == false)
                    {
                        _notyfService.Error("Tài khoản đã bị khóa");
                        return RedirectToAction("Login", "Users");
                    }

                    //Luu Session 
                    HttpContext.Session.SetString("userCurrentlyLogged", user.UserId.ToString());
                    var userCurrentlyLogged = HttpContext.Session.GetString("UsercurrentlyLogged");

                    //Identity
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim("userId", user.UserId.ToString())
                    };
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "login");
                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    await HttpContext.SignInAsync(claimsPrincipal);
                    _notyfService.Success("Đăng nhập thành công");
                    return RedirectToAction("Index", "Home");
                }
            }
            catch
            {
                return RedirectToAction("Register", "Users");
            }
            return View(loginViewModel);
        }
    }
}
