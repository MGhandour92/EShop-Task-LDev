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
        //TODO: 
        //apply discount on the cart page  
        //Place Order (Save order) 

        private readonly ApplicationDbContext _context;

        public ShoppingController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index(int? page, int? categoryId)
        {
            //for the first call
            categoryId = !categoryId.HasValue ? 0 : categoryId;

            var products = categoryId == 0
                ? _context.Product.Include(p => p.Category)
                : _context.Product.Include(p => p.Category).Where(c => c.CategoryId == categoryId);

            var CatList = _context.Category.Select(item => new SelectListItem(item.Name, item.Id.ToString())).ToList();
            CatList.Insert(0, new SelectListItem("All", "0"));
            CatList.FirstOrDefault(v => v.Value == categoryId.ToString()).Selected = true;

            ViewBag.CategoryList = CatList;

            int pageSize = 5;
            int pageNumber = (page ?? 1);

            var pagedProducts = await products.ToListAsync();

            return View(pagedProducts.ToPagedList(pageNumber, pageSize));
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PlaceOrder(OrderHeader order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();

                foreach (var item in order.OrderLines)
                {
                    //inserted ID
                    item.OrderHeaderId = order.Id;
                    //clear id before autoincrement
                    item.Id = 0;
                    _context.Add(item);
                }

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(OrderSummary), new { orderID = order.Id } );
            }
            return RedirectToAction("Index", "Cart");
        }

        // GET: order
        public async Task<IActionResult> OrderSummary(int orderID)
        {
            var order = await _context.OrderHeader
                                .Include(p => p.OrderLines)
                                .ThenInclude(x=>x.Product)
                                .FirstOrDefaultAsync(m => m.Id == orderID);

            return order == null ? NotFound() : View(order);
        }
    }
}
