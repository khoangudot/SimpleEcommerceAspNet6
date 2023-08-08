using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using SimpleEcommerceAspNet6.Data;
using SimpleEcommerceAspNet6.Models;

namespace SimpleEcommerceAspNet6.Areas.Manager.Controllers
{
    [Area("Manager")]
    
    public class ManageUsersController : Controller
    {
        private readonly EcommerceDbContext _context;
        private readonly INotyfService _notyf;

        public ManageUsersController(EcommerceDbContext context, INotyfService notyf)
        {
            _context = context;
            _notyf = notyf;
        }

        // GET: Manager/ManageUsers
        public async Task<IActionResult> Index(int? page)
        {

            ViewData["Roles"] = new SelectList(_context.Roles, "RoleId", "RoleName");
            List<SelectListItem> status = new List<SelectListItem>();
            status.Add(new SelectListItem() { Text = "Hoạt động", Value = "1" });
            status.Add(new SelectListItem() { Text = "Khóa", Value = "0" });
            ViewData["Status"] = status;
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 20;
            ViewBag.CurrentPage = pageNumber;

            var listUser = _context.Users
                .Include(u => u.Role_Users)
                .ThenInclude(ru => ru.Role)
                .AsNoTracking()
                .OrderBy(x => x.CreateDate);
            PagedList<User> models = new PagedList<User>(listUser, pageNumber, pageSize);
            return View(models);

        }
        // GET: Manager/ManageUsers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Manager/ManageUsers/Create
        public IActionResult Create()
        {
            ViewData["Roles"] = new SelectList(_context.Roles,"RoleId","RoleName");
            return View();
        }

        // POST: Manager/ManageUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,FullName,Phone,Email,Birthday,Avatar,Address,UserName,Password,Salt,Active,LastLogin,CreateDate")] User user, string[] RoleIds)
        {
            if (ModelState.IsValid)
            {
                if (!UserNameExists(user.UserName))
                {
                    user.CreateDate = DateTime.Now;
                    _context.Add(user);

                    await _context.SaveChangesAsync();
                    if (RoleIds.Length > 0)
                    {
                        Customer customer = new Customer();
                        foreach (string roleId in RoleIds)
                        {
                            if (int.TryParse(roleId, out int roleIdInt))
                            {
                                Role_User roleUser = new Role_User
                                {
                                    RoleId = roleIdInt,
                                    UserId = user.UserId
                                };
                                _context.Role_Users.Add(roleUser);
                                if (roleIdInt == 3)//role customer
                                {
                                    customer.UserId = user.UserId;
                                    _context.Customers.Add(customer);
                                }
                            }
                        }
                        await _context.SaveChangesAsync();
                    }
                    else
                    {

                        Role_User roleUser = new Role_User
                        {
                            RoleId = 3, //Role Customer
                            UserId = user.UserId
                        };
                        _context.Role_Users.Add(roleUser);
                        await _context.SaveChangesAsync();
                        Customer customer = new Customer
                        {
                            UserId = user.UserId
                        };
                        _context.Customers.Add(customer);
                        await _context.SaveChangesAsync();
                    }
                    _notyf.Success("Tạo mới thành công");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewData["Roles"] = new SelectList(_context.Roles, "RoleId", "RoleName");
                    _notyf.Error("Tên đăng nhập đã tồn tại");
                    return View(user);
                }
            }
            ViewData["Roles"] = new SelectList(_context.Roles, "RoleId", "RoleName");
            _notyf.Error("Tạo mới thất bại");
            return View(user);
        }

        // GET: Manager/ManageUsers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Manager/ManageUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,FullName,Phone,Email,Birthday,Avatar,Address,UserName,Password,Salt,Active,LastLogin,CreateDate")] User user)
        {
            if (id != user.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.UserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Manager/ManageUsers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Manager/ManageUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'EcommerceDbContext.Users'  is null.");
            }
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
          return (_context.Users?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
        private bool UserNameExists(string userName)
        {
            return (_context.Users?.Any(e => e.UserName.ToLower().Equals(userName.ToLower()) )).GetValueOrDefault();
        }
    }
}
