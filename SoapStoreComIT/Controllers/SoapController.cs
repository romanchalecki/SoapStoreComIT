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
    public class SoapController : Controller
    {
        private readonly ILogger<SoapController> _logger;
        private readonly ApplicationDbContext _db;

        public IEnumerable<Soap> Soaps { get; set; }

        public SoapController(ILogger<SoapController> logger,
            ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public async Task<IActionResult> SoapList() //display Soap List
        {
            SoapList results = new SoapList();
            Soaps = await _db.Soap.ToListAsync();
            results.Soaps = Soaps;
            return View(results);
        }

        public IActionResult CreateNewSoap() //create New Soap
        {
            Soap results = new Soap();
            return View(results);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddNewSoap(Soap _soap) //submit new Soap
        {
            if (ModelState.IsValid)
            {
                _db.Soap.Add(_soap);
                _db.SaveChanges();
                return Redirect(nameof(SoapList));
            }
            else
            {
                return View("CreateNewSoap");
            }
        }

        public async Task<IActionResult> EditSoap(int? Id) //return viev Edit Soap
        {
            if (Id == null)
            {
                return NotFound();
            }
            var _soap = await _db.Soap.FindAsync(Id);
            if (_soap == null)
            {
                return NotFound();
            }
            return View(_soap);
        }

        public async Task<IActionResult> SubmitEditSoap(Soap soap) //submit Edit Soap
        {
            if (ModelState.IsValid)
            {
                var SoapFromDb = await _db.Soap.FindAsync(soap.Id);
                SoapFromDb.Name = soap.Name;
                SoapFromDb.Weight = soap.Weight;
                SoapFromDb.Price = soap.Price;
                SoapFromDb.Description = soap.Description;
                SoapFromDb.Ingridients = soap.Ingridients;
                SoapFromDb.Quantity = soap.Quantity;

                await _db.SaveChangesAsync();
                return Redirect(nameof(SoapList));
            }
            else
            {
                return View("EditSoap");
            }
        }

        public async Task<IActionResult> DeleteSoap(int Id) //return viev Delete Soap
        {
            var _soap = await _db.Soap.FindAsync(Id);
            return View(_soap);
        }

        public IActionResult ConfirmDeleteSoap(int Id) //delete Soap
        {
            var _soap = _db.Soap.Find(Id);

            if (_soap == null)
            {
                return NotFound();
            }

            _db.Soap.Remove(_soap);
            _db.SaveChanges();
            return Redirect(nameof(SoapList));
        }
    }
}
