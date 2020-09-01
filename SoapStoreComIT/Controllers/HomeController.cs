using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SoapStoreComIT.Data;
using SoapStoreComIT.Models;
using SoapStoreComIT.Utility;
using SoapStoreComIT.Views.ViewModels;

namespace SoapStoreComIT.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger,
            ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        [BindProperty]
        public StoreItemViewModel StoreItemVM { get; set; }

        public IActionResult Index()
        {
            IndexViewModel IndexVM = new IndexViewModel()
            {
                StoreItem = _db.StoreItem.Include(m => m.Category).Include(m => m.SubCategory).ToList(),
                Category = _db.Category.ToList()
            };

            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);

            if(claim!=null)
            {
                var count = _db.ShoppingCart.Where(u => u.ApplicationUserId == claim.Value).ToList().Count;
                HttpContext.Session.SetInt32(SD.ssShoppingCartCount, count);
            }

            return View(IndexVM);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public async Task<IActionResult> MoreStoreItem(int? id)  //Get StoreItem Details / More
        {
            var storeItemFromDb = await _db.StoreItem.Include(m => m.Category).Include(m => m.SubCategory).Where(m => m.Id == id).FirstOrDefaultAsync();

            ShoppingCart cartObj = new ShoppingCart()
            {
                StoreItem = storeItemFromDb,
                StoreItemId = storeItemFromDb.Id
            };

            return View(cartObj);
        }


        //[Authorize]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> AddToCart(ShoppingCart cartObj, ShoppingCart CartObject)
        //{
        //    //CartObject.Id = 0;
        //    if (ModelState.IsValid)
        //    {
        //        var claimsIdentity = (ClaimsIdentity)this.User.Identity;
        //        var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
        //        CartObject.ApplicationUserId = claim.Value;

        //        ShoppingCart cartFromDb = await _db.ShoppingCart.Where(c => c.ApplicationUserId == CartObject.ApplicationUserId
        //                                                    && c.StoreItemId == CartObject.StoreItemId).FirstOrDefaultAsync();

        //        if (cartFromDb == null)
        //        {
        //            await _db.ShoppingCart.AddAsync(CartObject);
        //        }
        //        else
        //        {
        //            cartFromDb.Count = cartFromDb.Count + CartObject.Count;
        //        }
        //        await _db.SaveChangesAsync();

        //        var count = _db.ShoppingCart.Where(c => c.ApplicationUserId == CartObject.ApplicationUserId).ToList().Count();
        //        HttpContext.Session.SetInt32(SD.ssShoppingCartCount,count);

        //        return RedirectToAction("Index");
        //    }

        //    else
        //    {
        //        var storeItemFromDb = await _db.StoreItem.Include(m => m.Category).Include(m => m.SubCategory).Where(m => m.Id == CartObject.StoreItemId).FirstOrDefaultAsync();

        //        ShoppingCart cartObj = new ShoppingCart()
        //        {
        //            StoreItem = storeItemFromDb,
        //            StoreItemId = storeItemFromDb.Id
        //        };

        //        return View(cartObj);
        //    }
        //}


        [Authorize]
        //[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToCart(ShoppingCart CartObject, int id )
        {
            CartObject.StoreItemId = id;
            CartObject.Id = 0;

            if (ModelState.IsValid)
            {
                var claimsIdentity = (ClaimsIdentity)this.User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                CartObject.ApplicationUserId = claim.Value;

                ShoppingCart cartFromDb = await _db.ShoppingCart.Where(c => c.ApplicationUserId == CartObject.ApplicationUserId
                                                && c.StoreItemId == CartObject.StoreItemId).FirstOrDefaultAsync();

                if (cartFromDb == null)
                {
                    await _db.ShoppingCart.AddAsync(CartObject);
                }
                else
                {
                    cartFromDb.Count = cartFromDb.Count + CartObject.Count;
                }
                await _db.SaveChangesAsync();

                var count = _db.ShoppingCart.Where(c => c.ApplicationUserId == CartObject.ApplicationUserId).ToList().Count();
                HttpContext.Session.SetInt32(SD.ssShoppingCartCount, count);

                return RedirectToAction("Index");
            }
            else
            {

                var menuItemFromDb = await _db.StoreItem.Include(m => m.Category).Include(m => m.SubCategory).Where(m => m.Id == CartObject.StoreItemId).FirstOrDefaultAsync();

                ShoppingCart cartObj = new ShoppingCart()
                {
                    StoreItem = menuItemFromDb,
                    StoreItemId = menuItemFromDb.Id
                };

                return View(cartObj);
            }
        }
    }
}
