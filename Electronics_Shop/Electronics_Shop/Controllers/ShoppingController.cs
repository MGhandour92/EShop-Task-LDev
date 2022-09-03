using Electronics_Shop.Data;
using Electronics_Shop.Helpers;
using Electronics_Shop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using X.PagedList;

namespace Electronics_Shop.Controllers
{
    public class ShoppingController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ShoppingController(ApplicationDbContext context)
        {
            _context = context;
        }

        //Get all and filtered products for shopping
        public async Task<IActionResult> Index(int? page, int? categoryId)
        {
            //All Categories = 0
            categoryId = !categoryId.HasValue ? 0 : categoryId;

            //Filter by category
            var products = categoryId == 0
                ? _context.Product.Include(p => p.Category)
                : _context.Product.Include(p => p.Category).Where(c => c.CategoryId == categoryId);

            var CatList = _context.Category.Select(item => new SelectListItem(item.Name, item.Id.ToString())).ToList();
            CatList.Insert(0, new SelectListItem("All", "0"));
            //set last selected category
            CatList.FirstOrDefault(v => v.Value == categoryId.ToString()).Selected = true;

            ViewBag.CategoryList = CatList;

            //handle pagging
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            var pagedProducts = await products.ToListAsync();

            return View(pagedProducts.ToPagedList(pageNumber, pageSize));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        //Convert Cart to Order
        public async Task<IActionResult> PlaceOrder(OrderHeader order)
        {
            if (ModelState.IsValid && order.OrderLines.Count > 0)
            {
                //save order header
                _context.Add(order);
                await _context.SaveChangesAsync();

                //save order lines
                foreach (var item in order.OrderLines)
                {
                    //inserted ID
                    item.OrderHeaderId = order.Id;
                    //clear id before autoincrement
                    item.Id = 0;
                    _context.Add(item);
                }

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(OrderSummary), new { orderID = order.Id });
            }
            return RedirectToAction("Index", "Cart");
        }

        // GET: order
        public async Task<IActionResult> OrderSummary(int orderID)
        {
            //clear cart
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", new List<OrderLine>());

            //return order data
            var order = await _context.OrderHeader
                                .Include(p => p.OrderLines)
                                .ThenInclude(x => x.Product)
                                .FirstOrDefaultAsync(m => m.Id == orderID);

            ViewBag.total = order.OrderLines.Sum(item => item.Product.UnitPrice * item.Quantity);
            ViewBag.totalwdiscount = order.OrderLines.Sum(line => line.AfterDiscountPrice);

            return order == null ? NotFound() : View(order);
        }
    }
}
