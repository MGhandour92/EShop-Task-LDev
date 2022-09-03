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

            //calcualte discount
            //for each line apply two piece discount
            cart.ForEach(dis => dis.AfterDiscountPrice = 
            DiscountHandler.TwoPieceDisount(dis.Product.UnitPrice, dis.Quantity, dis.Product.DisountToApply));

            //ViewBag.cart = cart;
            //calculate total after discount
            ViewBag.total = cart.Sum(item => item.Product.UnitPrice * item.Quantity);
            ViewBag.totalwdiscount = cart.Sum(line => line.AfterDiscountPrice);

            var oh = new OrderHeader()
            {
                OrderLines = cart
            };

            return View(oh);
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
                int index = isExist(ProdId);
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


        public IActionResult Remove(int id)
        {
            List<OrderLine> cart = SessionHelper.GetObjectFromJson<List<OrderLine>>(HttpContext.Session, "cart");
            int index = isExist(id);
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
        }



        //If product already added
        private int isExist(int id)
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
