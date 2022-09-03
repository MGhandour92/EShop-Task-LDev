using Electronics_Shop.Data;
using Electronics_Shop.Helpers;
using Electronics_Shop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Electronics_Shop.Controllers
{
    public class CartController : Controller
    {

        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var cart = SessionHelper.GetObjectFromJson<List<OrderLine>>(HttpContext.Session, "cart");
            //ViewBag.cart = cart;
            ViewBag.total = cart.Sum(item => item.Product.UnitPrice * item.Quantity);

            var ovm = new OrderViewModel()
            {
                OrderLines = cart
            };
            return View(ovm);
        }

        // AddToCart
        public IActionResult AddToCart(int ProdId, int qtyToAdd)
        {
            Product productModel = new Product();

            if (SessionHelper.GetObjectFromJson<List<OrderLine>>(HttpContext.Session, "cart") == null)
            {
                List<OrderLine> cart = new List<OrderLine>();
                cart.Add(new OrderLine { Product = _context.Product.Find(ProdId), Quantity = Convert.ToInt32(qtyToAdd) });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<OrderLine> cart = SessionHelper.GetObjectFromJson<List<OrderLine>>(HttpContext.Session, "cart");
                int index = isExist(ProdId.ToString());
                if (index != -1)
                {
                    cart[index].Quantity += Convert.ToInt32(qtyToAdd);
                }
                else
                {
                    cart.Add(new OrderLine { Product = _context.Product.Find(ProdId), Quantity = Convert.ToInt32(qtyToAdd) });
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Index");
        }


        public IActionResult Remove(string id)
        {
            List<OrderLine> cart = SessionHelper.GetObjectFromJson<List<OrderLine>>(HttpContext.Session, "cart");
            int index = isExist(id);
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
        }



        //If product already added
        private int isExist(string id)
        {
            List<OrderLine> cart = SessionHelper.GetObjectFromJson<List<OrderLine>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Product.Id.Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }


    }
}
