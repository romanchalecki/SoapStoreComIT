using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoapStoreComIT.Areas.Identity.Data;
using SoapStoreComIT.Data;
using SoapStoreComIT.Utility;

namespace SoapStoreComIT.Controllers
{
    [Authorize(Roles = SD.ManagerUser)]
    public class UserController : Controller
    {
        private readonly SoapStoreComITIdentityDbContext _db;

        public UserController(SoapStoreComITIdentityDbContext db)
        {
            _db = db;

        }

        public async Task<IActionResult> UserList()
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var results = await _db.ApplicationUser.Where(u => u.Id != claim.Value).ToListAsync();

            return View(results);
        }

        public async Task<IActionResult> Lock(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationUser = await _db.ApplicationUser.FirstOrDefaultAsync(m => m.Id==id);

            if(applicationUser==null)
            {
                return NotFound();
            }

            applicationUser.LockoutEnd = DateTime.Now.AddYears(500);

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(UserList));
        }

        public async Task <IActionResult> UnLock(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationUser = await _db.ApplicationUser.FirstOrDefaultAsync(m => m.Id == id);

            if (applicationUser == null)
            {
                return NotFound();
            }

            applicationUser.LockoutEnd = DateTime.Now;

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(UserList));
        }
    }
}
