using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoapStoreComIT.Areas.Identity.Data;
using SoapStoreComIT.Data;
using SoapStoreComIT.Models;
using SoapStoreComIT.Utility;
using SoapStoreComIT.Views.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SoapStoreComIT.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly SoapStoreComITIdentityDbContext __db;

        [BindProperty]
        public CartOrderDetails detailCart { get; set; }

        public CartController(ApplicationDbContext db, SoapStoreComITIdentityDbContext identityDb)
        {
            _db = db;
            __db = identityDb;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            detailCart = new CartOrderDetails()
            {
                Order = new Models.Order()
            };

            detailCart.Order.OrderTotal = 0;

            var claimsIdentity = (ClaimsIdentity)User.Identity;

            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var cart = _db.ShoppingCart.Where(c => c.ApplicationUserId == claim.Value);

            if (cart != null)
            {
                detailCart.listCart = cart.ToList();
            }

            foreach (var list in detailCart.listCart)
            {
                list.StoreItem = await _db.StoreItem.FirstOrDefaultAsync(m => m.Id == list.StoreItemId);
                if (list.StoreItem != null)
                    detailCart.Order.OrderTotal = detailCart.Order.OrderTotal + (list.StoreItem.Price * list.Count);
            }


            return View(detailCart);
        }

        public async Task<IActionResult> Plus(int? cartId)
        {
            var cart = await _db.ShoppingCart.FirstOrDefaultAsync(c => c.Id == cartId);
            cart.Count += 1;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Minus(int? cartId)
        {
            var cart = await _db.ShoppingCart.FirstOrDefaultAsync(c => c.Id == cartId);
            if (cart.Count == 1)
            {
                _db.ShoppingCart.Remove(cart);
                await _db.SaveChangesAsync();

                var cnt = _db.ShoppingCart.Where(u => u.ApplicationUserId == cart.ApplicationUserId).ToList().Count;
                HttpContext.Session.SetInt32(SD.ssShoppingCartCount, cnt);
            }
            else
            {
                cart.Count -= 1;
                await _db.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Remove(int? cartId)
        {
            var cart = await _db.ShoppingCart.FirstOrDefaultAsync(c => c.Id == cartId);
            _db.ShoppingCart.Remove(cart);
            await _db.SaveChangesAsync();

            var cnt = _db.ShoppingCart.Where(u => u.ApplicationUserId == cart.ApplicationUserId).ToList().Count;
            HttpContext.Session.SetInt32(SD.ssShoppingCartCount, cnt);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Summary()
        {
            detailCart = new CartOrderDetails()
            {
                Order = new Models.Order()
            };

            detailCart.Order.OrderTotal = 0;

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            ApplicationUser applicationUser = await __db.ApplicationUser.Where(c => c.Id == claim.Value).FirstOrDefaultAsync();

            var cart = _db.ShoppingCart.Where(c => c.ApplicationUserId == claim.Value);

            if (cart != null)
            {
                detailCart.listCart = cart.ToList();
            }

            foreach (var list in detailCart.listCart)
            {
                list.StoreItem = await _db.StoreItem.FirstOrDefaultAsync(m => m.Id == list.StoreItemId);
                if (list.StoreItem != null)
                    detailCart.Order.OrderTotal = detailCart.Order.OrderTotal + (list.StoreItem.Price * list.Count);
            }

            detailCart.Order.PickupName = $"{applicationUser.FirstName} {applicationUser.LastName}";
            detailCart.Order.PhoneNumber = applicationUser.PhoneNumber;


            return View(detailCart);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Summary")]
        public async Task<IActionResult> SummaryPost()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            detailCart.listCart = await _db.ShoppingCart.Where(c => c.ApplicationUserId == claim.Value).ToListAsync();

            detailCart.Order.PaymentStatus = "Payment pending";
            detailCart.Order.OrderDate = DateTime.Now;
            detailCart.Order.UserId = claim.Value;
            detailCart.Order.Status = "Payment pending";

            List<OrderDetails> orderDetailsList = new List<OrderDetails>();
            _db.Order.Add(detailCart.Order);
            await _db.SaveChangesAsync();


            foreach (var item in detailCart.listCart)
            {
                item.StoreItem = await _db.StoreItem.FirstOrDefaultAsync(m => m.Id == item.StoreItemId);
                OrderDetails orderDetails = new OrderDetails
                {
                    StoreItemId = item.StoreItemId,
                    OrderId = detailCart.Order.Id,
                    Name = item.StoreItem.Name,
                    Price = item.StoreItem.Price,
                    Count = item.Count
                };
                detailCart.Order.OrderTotal += orderDetails.Count * orderDetails.Price;
                _db.OrderDetails.Add(orderDetails);
            }
            detailCart.Order.OrderTotal = detailCart.Order.OrderTotal + detailCart.Order.DeliveryCost;
            await _db.SaveChangesAsync();

            _db.ShoppingCart.RemoveRange(detailCart.listCart);
            HttpContext.Session.SetInt32(SD.ssShoppingCartCount, 0);

            await _db.SaveChangesAsync();

            return RedirectToAction("Chechout", "Cart", new { id = detailCart.Order.Id });
            //return RedirectToAction ("Confirm", "Order", new { id=detailCart.Order.Id});
        }



        public async Task<IActionResult> Chechout(int id)
        {
            OrderPaymentDetails paymentDetails = new OrderPaymentDetails();

            var _order = await _db.Order.SingleOrDefaultAsync(o => o.Id == id);

            if(_order != null)
            { 
                paymentDetails.Order = _order;
            }
            //paymentDetails.Order = await _db.Order.SingleOrDefaultAsync(m => m.Id == id);

            return View(paymentDetails);
        }


        public async Task<IActionResult> PayNow(int id)
        {
            var order = await _db.Order.SingleOrDefaultAsync(m => m.Id == id);
            order.PaymentStatus = "PaymentComplited";
            order.Status = "PaymentComplited";

            await _db.SaveChangesAsync();

            return View("PaymentComplited");
        }
    }
}
