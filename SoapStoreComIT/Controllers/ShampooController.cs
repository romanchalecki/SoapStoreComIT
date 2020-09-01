using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SoapStoreComIT.Data;
using SoapStoreComIT.Models;
using SoapStoreComIT.Utility;
using SoapStoreComIT.Views.ViewModels;

namespace SoapStoreComIT.Controllers
{
    [Authorize(Roles = SD.ManagerUser)]
    public class ShampooController : Controller
    {
        private readonly ILogger<ShampooController> _logger;
        private readonly ApplicationDbContext _db;
        public IEnumerable<Shampoo> Shampoos { get; set; }

        public ShampooController(ILogger<ShampooController> logger,
            ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }


        public async Task<IActionResult> ShampooList() //display Shampoo List
        {
            ShampooList results = new ShampooList();
            Shampoos = await _db.Shampoo.ToListAsync();
            results.Shampoos = Shampoos;
            return View(results);
        }

        public IActionResult CreateNewShampoo() //create New Shampoo
        {
            Shampoo results = new Shampoo();
            return View(results);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddNewShampoo(Shampoo _shampoo) //submit new Shampoo
        {
            if (ModelState.IsValid)
            {
                _db.Shampoo.Add(_shampoo);
                _db.SaveChanges();
                return Redirect(nameof(ShampooList));
            }
            else
            {
                return View("CreateNewShampoo");
            }
        }

        public async Task<IActionResult> EditShampoo(int? Id) //return viev Edit Shampoo
        {
            if (Id == null)
            {
                return NotFound();
            }
            var _shampoo = await _db.Shampoo.FindAsync(Id);
            if (_shampoo == null)
            {
                return NotFound();
            }
            return View(_shampoo);
        }

        public async Task<IActionResult> SubmitEditShampoo(Shampoo shampoo) //submit Edit Shampoo
        {
            if (ModelState.IsValid)
            {
                var ShampooFromDb = await _db.Shampoo.FindAsync(shampoo.Id);
                ShampooFromDb.Name = shampoo.Name;
                ShampooFromDb.Contents = shampoo.Contents;
                ShampooFromDb.Price = shampoo.Price;
                ShampooFromDb.Description = shampoo.Description;
                ShampooFromDb.Ingridients = shampoo.Ingridients;
                ShampooFromDb.Quantity = shampoo.Quantity;

                await _db.SaveChangesAsync();
                return Redirect(nameof(ShampooList));
            }
            else
            {
                return View("EditShampoo");
            }
        }

        public async Task<IActionResult> DeleteShampoo(int Id) //return viev Delete Shampoo
        {
            var _shampoo = await _db.Shampoo.FindAsync(Id);
            return View(_shampoo);
        }

        public IActionResult ConfirmDeleteShampoo(int Id) //delete Shampoo
        {
            var _shampoo = _db.Shampoo.Find(Id);

            if (_shampoo == null)
            {
                return NotFound();
            }

            _db.Shampoo.Remove(_shampoo);
            _db.SaveChanges();
            return Redirect(nameof(ShampooList));
        }
    }
}