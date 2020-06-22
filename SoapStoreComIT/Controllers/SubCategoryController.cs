using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SoapStore.Models;
using SoapStoreComIT.Migrations;
using SoapStoreComIT.Views.ViewModels;

namespace SoapStoreComIT.Controllers
{
    public class SubCategoryController : Controller
    {
        private readonly ILogger<SubCategoryController> _logger;
        private readonly ApplicationDbContext _db;

        //public IEnumerable<SubCategory> SubCategories { get; set; }


        public SubCategoryController(ILogger<SubCategoryController> logger,
            ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public async Task<IActionResult> SubCategoryList() //display SubCategory List
        {
            SubCategoryList results = new SubCategoryList();
            var SubCategories = await _db.SubCategory.Include(s=>s.Category).ToListAsync();
            results.SubCategories = SubCategories;
            return View(results);
        }
    }
}
