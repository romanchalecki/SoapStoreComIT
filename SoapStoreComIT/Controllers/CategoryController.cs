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
    public class CategoryController : Controller
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly ApplicationDbContext _db;

        public IEnumerable<Category> Categories { get; set; }


        public CategoryController(ILogger<CategoryController> logger,
            ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }


        public async Task<IActionResult> CategoryList() //display Category List
        {
            CategoryList results = new CategoryList();
            Categories = await _db.Category.ToListAsync();
            results.Categories = Categories;
            return View(results);
        }

        public IActionResult CreateNewCategory() //create New Category
        {
            Category results = new Category ();
            return View(results);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddNewCategory(Category _category) //submit new Category
        {
            if (ModelState.IsValid)
            {
                _db.Category.Add(_category);
                _db.SaveChanges();
                return Redirect(nameof(CategoryList));
            }
            else
            {
                return View("CreateNewCategory");
            }
        }

        public async Task<IActionResult> EditCategory(int? Id) //return viev Edit Category
        {
            if (Id == null)
            {
                return NotFound();
            }
            var _category = await _db.Category.FindAsync(Id);
            if (_category == null)
            {
                return NotFound();
            }
            return View(_category);
        }

        public async Task<IActionResult> SubmitEditCategory(Category category) //submit Edit Soap
        {
            if (ModelState.IsValid)
            {
                var CategoryFromDb = await _db.Category.FindAsync(category.Id);
                CategoryFromDb.Name = category.Name;

                await _db.SaveChangesAsync();
                return Redirect(nameof(CategoryList));
            }
            else
            {
                return View("EditCategory");
            }
        }

        public async Task<IActionResult> DeleteCategory(int Id) //return viev Delete Category
        {
            var _category = await _db.Category.FindAsync(Id);
            return View(_category);
        }

        public IActionResult ConfirmDeleteCategory(int Id) //delete Category
        {
            var _category = _db.Category.Find(Id);

            if (_category == null)
            {
                return NotFound();
            }

            _db.Category.Remove(_category);
            _db.SaveChanges();
            return Redirect(nameof(CategoryList));
        }


    }
}