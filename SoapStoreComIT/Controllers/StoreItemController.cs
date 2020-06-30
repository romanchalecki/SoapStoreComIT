using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SoapStore.Models;
using SoapStoreComIT.Views.ViewModels;


namespace SoapStoreComIT.Controllers
{
    public class StoreItemController : Controller
    {
        private readonly ApplicationDbContext _db;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public StoreItemController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> StoreItemList() // display StoreItem List
        {
            var storeItems = await _db.StoreItem.Include(m => m.Category).Include(m => m.SubCategory).ToListAsync();
            return View(storeItems);
        }
    }
}
