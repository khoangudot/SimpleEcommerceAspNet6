using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using AspNetCoreHero.ToastNotification.Notyf;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SimpleEcommerceAspNet6.Data;
using SimpleEcommerceAspNet6.Models;

namespace SimpleEcommerceAspNet6.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class ManageRolesController : Controller
    {
        private readonly EcommerceDbContext _context;
        private readonly INotyfService _notyfService;

        public ManageRolesController(EcommerceDbContext context, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
        }

        // GET: Manager/ManageRoles
        public async Task<IActionResult> Index()
        {
              return _context.Roles != null ? 
                          View(await _context.Roles.ToListAsync()) :
                          Problem("Entity set 'EcommerceDbContext.Role'  is null.");
        }

        // GET: Manager/ManageRoles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Roles == null)
            {
                return NotFound();
            }

            var role = await _context.Roles
                .FirstOrDefaultAsync(m => m.RoleId == id);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        // GET: Manager/ManageRoles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Manager/ManageRoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoleId,RoleName,Description")] Role role)
        {
            if (ModelState.IsValid)
            {
                if (!RoleNameExists(role.RoleName))
                {
                    _context.Add(role);
                    await _context.SaveChangesAsync();
                    _notyfService.Success("Tạo mới thành công");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _notyfService.Error("Tên quyền truy cập đã tồn tại");
                }
               
            }
            _notyfService.Error("Tạo mới thất bại");
            return View(role);
        }

        // GET: Manager/ManageRoles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Roles == null)
            {
                _notyfService.Error("ID không tồn tại");
                return NotFound();
            }

            var role = await _context.Roles.FindAsync(id);
            if (role == null)
            {
                _notyfService.Error("Không tìm thấy quyền truy cập");
                return NotFound();
            }
            return View(role);
        }

        // POST: Manager/ManageRoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RoleId,RoleName,Description")] Role role)
        {
            if (id != role.RoleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(role);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoleExists(role.RoleId))
                    {
                        return NotFound();
                    }
                    
                    else
                    {
                        throw;
                    }
                }
                _notyfService.Success("Chỉnh sửa thành công");
                return RedirectToAction(nameof(Index));
            }
            return View(role);
        }

        // GET: Manager/ManageRoles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Roles == null)
            {
                return NotFound();
            }

            var role = await _context.Roles
                .FirstOrDefaultAsync(m => m.RoleId == id);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        // POST: Manager/ManageRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Roles == null)
            {
                return Problem("Entity set 'EcommerceDbContext.Role'  is null.");
            }
            var role = await _context.Roles.FindAsync(id);
            if (role != null)
            {
                _context.Roles.Remove(role);
            }
            
            await _context.SaveChangesAsync();
            _notyfService.Success("Xóa thành công");
            return RedirectToAction(nameof(Index));
        }

        private bool RoleExists(int id)
        {
          return (_context.Roles?.Any(e => e.RoleId == id)).GetValueOrDefault();
        }
        private bool RoleNameExists(string roleName)
        {
            return (_context.Roles?.Any(e => e.RoleName.ToLower().Equals(roleName.ToLower()))).GetValueOrDefault();
        }
    }
}
