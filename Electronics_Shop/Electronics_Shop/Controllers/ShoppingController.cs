using Electronics_Shop.Data;
using Electronics_Shop.Helpers;
using Electronics_Shop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace Electronics_Shop.Controllers
{
    public class ShoppingController : Controller
    {
        //TODO: 
        //apply discount on the cart page  
        Place Order (Save order) 

        private readonly ApplicationDbContext _context;

        public ShoppingController(ApplicationDbContext context)
        {
            _context = context;
        }

        //Products
        private IQueryable<Product> GetProducts(int catId) =>
    catId == 0
        ? _context.Product.Include(p => p.Category)
        : _context.Product.Include(p => p.Category).Where(c => c.CategoryId == catId);

        // GET: Products
        public async Task<IActionResult> Index(int? page)
        {
            var products = GetProducts(0);

            var CatList = _context.Category.Select(item => new SelectListItem(item.Name, item.Id.ToString())).ToList();
            CatList.Insert(0, new SelectListItem("All", "0"));
            ViewBag.CategoryList = CatList;


            int pageSize = 5;
            int pageNumber = (page ?? 1);

            var pagedProducts = await products.ToListAsync();

            return View(pagedProducts.ToPagedList(pageNumber, pageSize));
        }

        // POST: Products
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(int? page, string categoryId)
        {
            var products = GetProducts(Convert.ToInt16(categoryId));

            var CatList = _context.Category.Select(item => new SelectListItem(item.Name, item.Id.ToString())).ToList();
            CatList.Insert(0, new SelectListItem("All", "0"));
            CatList.FirstOrDefault(v => v.Value == categoryId).Selected = true;
            ViewBag.CategoryList = CatList;

            var pagedProducts = await products.ToListAsync();

            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(pagedProducts.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PlaceOrder(OrderViewModel order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order.OrderHeader);
                await _context.SaveChangesAsync();

                foreach (var item in order.OrderLines)
                {
                    //inserted ID
                    item.OrderHeaderId = order.OrderHeader.Id;
                }

                _context.Add(order.OrderLines);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(OrderSummary));
            }
            return RedirectToAction("Index", "Cart");
        }

        // GET: order
        public IActionResult OrderSummary(OrderViewModel order) => View(order);
    }
}
