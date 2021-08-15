using Ecommerce.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Controllers
{
    [Authorize]
    public class CartsController : Controller
    {
        readonly EcomContext _db = new EcomContext();
        // GET: Carts
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var cart = _db.Carts.FirstOrDefault(q => q.UserId == userId);
            var cartItems = new List<CartItem>();

            if (cart != null)
                cartItems = _db.CartItems.Where(q => q.CartId == cart.Id).Include(q => q.Product).ToList();

            return View(cartItems);
        }
        
        [HttpPost]
        public ActionResult AddToCart(ProductCartModel model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Details", "Products", new { id = model.Product.Id });

            var userId = User.Identity.GetUserId();
            var cart = _db.Carts.FirstOrDefault(q => q.UserId == userId);
            if (cart == null)
            {
                cart = new Cart { UserId = userId };
                _db.Carts.Add(cart);
                _db.SaveChanges();
            }

            var cartItem = new CartItem {
                CartId = cart.Id,
                ProductId = model.Product.Id,
                Quantity = model.Quantity,
                Price = model.Quantity * model.Product.Price, 
            };
            _db.CartItems.Add(cartItem);
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public ActionResult GetItemsCount()
        {
            var userId = User.Identity.GetUserId();
            var cart = _db.Carts.FirstOrDefault(q => q.UserId == userId);
            var cartItems = new List<CartItem>();

            if (cart != null)
                cartItems = _db.CartItems.Where(q => q.CartId == cart.Id).Include(q => q.Product).ToList();

            return Json(cartItems.Count, JsonRequestBehavior.AllowGet);
        }
    }
}