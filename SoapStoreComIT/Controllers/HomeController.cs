using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SoapStore.Models;
using SoapStoreComIT.Models;
using SoapStoreComIT.Views.ViewModels;

namespace SoapStoreComIT.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;
        public IEnumerable<Shampoo> Shampoos { get; set; }
        public IEnumerable<Soap> Soaps { get; set; }

        public HomeController(ILogger<HomeController> logger,
            ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
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
            if(Id==null)
            {
                return NotFound();
            }
            var _soap = await _db.Soap.FindAsync(Id);
            if(_soap==null)
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


        public async Task<IActionResult> ShampooList() //display Shampoo List
        {
            ShampooList results = new ShampooList();
            Shampoos = await _db.Shampoo.ToListAsync();
            results.Shampoos = Shampoos;
            return View(results);
        }

        public IActionResult CreateNewShampoo() //create New Shampoo
        {
            Shampoo results = new Shampoo ();
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
            if(_shampoo==null)
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
