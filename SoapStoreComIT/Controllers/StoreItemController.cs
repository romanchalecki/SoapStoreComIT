using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SoapStore.Models;
using SoapStoreComIT.Utility;
using SoapStoreComIT.Views.ViewModels;



namespace SoapStoreComIT.Controllers
{
    public class StoreItemController : Controller
    {
        private readonly ApplicationDbContext _db;

        private readonly IWebHostEnvironment _webHostEnvironment;

        [BindProperty]
        public StoreItemViewModel StoreItemVM { get; set; }

        public StoreItemController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;

            StoreItemVM = new StoreItemViewModel()
            {
                Category = db.Category,
                StoreItem = new Models.StoreItem()
            };
        }


        public async Task<IActionResult> StoreItemList() // display StoreItem List
        {
            var storeItems = await _db.StoreItem.Include(m => m.Category).Include(m => m.SubCategory).ToListAsync();
            return View(storeItems);
        }


        public IActionResult CreateNewStoreItem()  //Get Create New StoreItem
        {
            return View(StoreItemVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddNewStoreItem() //Save New StoreItem to DB
        {
            StoreItemVM.StoreItem.SubCategoryId = Convert.ToInt32(Request.Form["SubCategoryId"].ToString());

            if(!ModelState.IsValid)
            {
                return View(StoreItemVM);
            }

            _db.StoreItem.Add(StoreItemVM.StoreItem);
            _db.SaveChanges();

            string webRootPath = _webHostEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            var storeItemFromDb = _db.StoreItem.Find(StoreItemVM.StoreItem.Id);

            if (files.Count > 0)
            {
                var uploads = Path.Combine(webRootPath, "images");
                var extension = Path.GetExtension(files[0].FileName);
                using (var filesStream = new FileStream(Path.Combine(uploads, StoreItemVM.StoreItem.Id + extension), FileMode.Create))
                {
                    files[0].CopyTo(filesStream);
                }
                storeItemFromDb.Image = @"\images\" + StoreItemVM.StoreItem.Id + extension;
            }
            //else
            //{
            //    var uploads = Path.Combine(webRootPath, @"images/" + SD.DeafultItemImage);
            //    System.IO.File.Copy(uploads, webRootPath + @"\images\" + StoreItemVM.StoreItem.Id + ".png");
            //    storeItemFromDb.Image = @"\images\" + StoreItemVM.StoreItem.Id + ".png";
            //}

            _db.SaveChanges();

            return RedirectToAction(nameof(StoreItemList));
        }


        public IActionResult EditStoreItem(int? id)  //Get Edit New StoreItem
        {
            if(id==null)
            { return NotFound(); }

            StoreItemVM.StoreItem = _db.StoreItem.Include(m => m.Category).Include(m => m.SubCategory).SingleOrDefault(m => m.Id == id);
            StoreItemVM.SubCategory = _db.SubCategory.Where(s => s.CategoryId == StoreItemVM.StoreItem.CategoryId).ToList();

            if(StoreItemVM.StoreItem==null)
            { return NotFound(); }
            return View(StoreItemVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveEditStoreItem(int? id) //Save Edit StoreItem to DB // NIE DZIAŁA PRAWIDŁOWO - GUBI ID I NIE ZAPISUJE ZMIAN
        {
            if(id == null)
            { return NotFound(); }

            StoreItemVM.StoreItem.SubCategoryId = Convert.ToInt32(Request.Form["SubCategoryId"].ToString());

            if (!ModelState.IsValid)
            {
                StoreItemVM.SubCategory = _db.SubCategory.Where(s => s.CategoryId == StoreItemVM.StoreItem.CategoryId).ToList();
                return View(StoreItemVM);
            }

            string webRootPath = _webHostEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            var storeItemFromDb = _db.StoreItem.Find(StoreItemVM.StoreItem.Id);//

            if (files.Count > 0)
            {
                var uploads = Path.Combine(webRootPath, "images");
                var extension_new = Path.GetExtension(files[0].FileName);

                var imagePath = Path.Combine(webRootPath, storeItemFromDb.Image.TrimStart('\\'));

                if (System.IO.File.Exists(imagePath))
                { System.IO.File.Delete(imagePath);}

                using (var filesStream = new FileStream(Path.Combine(uploads, StoreItemVM.StoreItem.Id + extension_new), FileMode.Create))
                {
                    files[0].CopyTo(filesStream);
                }
                storeItemFromDb.Image = @"\images\" + StoreItemVM.StoreItem.Id + extension_new;
            }


            storeItemFromDb.Name = StoreItemVM.StoreItem.Name;
            storeItemFromDb.Price = StoreItemVM.StoreItem.Price;
            storeItemFromDb.Description = StoreItemVM.StoreItem.Description;
            storeItemFromDb.Weight = StoreItemVM.StoreItem.Weight;
            storeItemFromDb.Ingridients = StoreItemVM.StoreItem.Ingridients;
            storeItemFromDb.Quantity = StoreItemVM.StoreItem.Quantity;
            storeItemFromDb.CategoryId = StoreItemVM.StoreItem.CategoryId;
            storeItemFromDb.SubCategoryId = StoreItemVM.StoreItem.SubCategoryId;

            _db.SaveChanges();

            return RedirectToAction(nameof(StoreItemList));
        }



        public IActionResult DetailsStoreItem (int? id)  //Get StoreItem Details
        {
            if (id == null)
            { return NotFound(); }

            StoreItemVM.StoreItem = _db.StoreItem.Include(m => m.Category).Include(m => m.SubCategory).SingleOrDefault(m => m.Id == id);
            StoreItemVM.SubCategory = _db.SubCategory.Where(s => s.CategoryId == StoreItemVM.StoreItem.CategoryId).ToList();

            if (StoreItemVM.StoreItem == null)
            { return NotFound(); }
            return View(StoreItemVM);
        }


        public IActionResult DeleteStoreItem(int? id)  //Get StoreItem for Delete
        {
            if (id == null)
            { return NotFound(); }

            StoreItemVM.StoreItem = _db.StoreItem.Include(m => m.Category).Include(m => m.SubCategory).SingleOrDefault(m => m.Id == id);
            StoreItemVM.SubCategory = _db.SubCategory.Where(s => s.CategoryId == StoreItemVM.StoreItem.CategoryId).ToList();

            if (StoreItemVM.StoreItem == null)
            { return NotFound(); }
            return View(StoreItemVM);
        }


        public IActionResult SubmitDeleteStoreItem (int? id) //submit Delete StoreItem
        {
            var _storeitem = _db.StoreItem.Find(StoreItemVM.StoreItem.Id);

            if (_storeitem == null)
            {
                return NotFound();
            }

            _db.StoreItem.Remove(_storeitem);
            _db.SaveChanges();
            return Redirect(nameof(StoreItemList));
        }
    }
}
