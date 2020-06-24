using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SoapStore.Models;
using SoapStoreComIT.Models;
using SoapStoreComIT.Views.ViewModels;

namespace SoapStoreComIT.Controllers
{
    public class SubCategoryController : Controller
    {
        private readonly ILogger<SubCategoryController> _logger;
        private readonly ApplicationDbContext _db;

        [TempData]
        public string StatusMessage { get; set; }

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

        public async Task<IActionResult> CreateNewSubCategory() //create New SubCategory
        {
            SubCategoryList results = new SubCategoryList()
            {
                CategoryList = await _db.Category.ToListAsync(),
                SubCategory = new Models.SubCategory(),
                SubCategoryLists = await _db.SubCategory.OrderBy(p => p.Name).Select(p => p.Name).Distinct().ToListAsync()
            };
            return View(results);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddNewSubCategory(SubCategoryList _subcategory) //submit new SubCategory
        {
            if (ModelState.IsValid)
            {
                var doesSubCategoryExists = _db.SubCategory.Include(s => s.Category).Where(s => s.Name == _subcategory.SubCategory.Name && s.Category.Id ==_subcategory.SubCategory.CategoryId);
                if (doesSubCategoryExists.Count()>0)
                {
                    //ERROR DOESEN'T WORK 
                    StatusMessage = "Error: SubCategory already exist under " + doesSubCategoryExists.First().Category.Name + " category. Please use another name.";
                }
                else
                {
                    _db.SubCategory.Add(_subcategory.SubCategory);
                    _db.SaveChanges();
                    return Redirect(nameof(SubCategoryList));
                }
            }

            SubCategoryList _subCategoryVM = new SubCategoryList()
            {
                CategoryList = _db.Category.ToList(),
                SubCategory = _subcategory.SubCategory,
                SubCategoryLists = _db.SubCategory.OrderBy(p => p.Name).Select(p => p.Name).ToList(),
                StatusMessage = StatusMessage
            };
            return View("CreateNewSubCategory");
        }


        [ActionName("GetSubcategory")]
        public async Task<IActionResult> GetSubCategory(int id)
        {
            List<SubCategory> subCategories = new List<SubCategory>();

            subCategories = await (from subCategory in _db.SubCategory
                             where subCategory.CategoryId == id
                             select subCategory).ToListAsync();
            return Json(new SelectList(subCategories, "Id", "Name"));
        }


        public async Task<IActionResult> EditSubCategory(int? id) //Edit SubCategory
        {
            if(id==null)
            {
                return NotFound();
            }
            var subCategory = await _db.SubCategory.SingleOrDefaultAsync(m => m.Id == id);

            if (subCategory  == null)
            {
                return NotFound();
            }

            SubCategoryList results = new SubCategoryList()
            {
                CategoryList = await _db.Category.ToListAsync(),
                SubCategory = subCategory,
                SubCategoryLists = await _db.SubCategory.OrderBy(p => p.Name).Select(p => p.Name).Distinct().ToListAsync()
            };
            return View(results);
        }


        public IActionResult SubmitEditSubCategory(SubCategory subCategory) //submit Edit SubCategory
        {
            if (ModelState.IsValid)
            {

                var subCategoryFromDb = _db.SubCategory.Find(subCategory.Id);
                subCategoryFromDb.Name = subCategory.Name;

                _db.SaveChanges();
                return Redirect(nameof(SubCategoryList));

            }

            else
            {
                return View("CreateNewSubCategory");
            }
        }




    }
}


