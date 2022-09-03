using Electronics_Shop.Data;
using Electronics_Shop.Helpers;
using Electronics_Shop.Models;
using Microsoft.AspNetCore.Authorization;
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
        
        //View cart items
        public IActionResult Index()
        {
            //read the cart session saved earlier
            var cart = SessionHelper.GetObjectFromJson<List<OrderLine>>(HttpContext.Session, "cart");

            //calcualte discount for each line apply two piece discount
            cart.ForEach(dis => dis.AfterDiscountPrice = 
            DiscountHandler.TwoPieceDisount(dis.Product.UnitPrice, dis.Quantity, dis.Product.DisountToApply));

            //calculate total after discount
            ViewBag.total = cart.Sum(item => item.Product.UnitPrice * item.Quantity);
            //calculate total after discount
            ViewBag.totalwdiscount = cart.Sum(line => line.AfterDiscountPrice);

            var oh = new OrderHeader()
            {
                OrderLines = cart
            };

            return View(oh);
        }

        //Add product to the cart
        public IActionResult AddToCart(int ProdId, int qtyToAdd)
        {
            //if cart is empty
            if (SessionHelper.GetObjectFromJson<List<OrderLine>>(HttpContext.Session, "cart") == null)
            {
                //add items
                var cart = new List<OrderLine>
                {
                    new OrderLine { Product = _context.Product.Find(ProdId), Quantity = Convert.ToInt32(qtyToAdd) }
                };
                //save as new cart
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {   //cart session found
                List<OrderLine> cart = SessionHelper.GetObjectFromJson<List<OrderLine>>(HttpContext.Session, "cart");
                
                //check if existed earlier
                int index = isExist(ProdId);
                
                if (index != -1)
                    //if the newly added product is already found in the cart items then increase quantity
                    cart[index].Quantity += Convert.ToInt32(qtyToAdd);
                else
                    //else add as new item
                    cart.Add(new OrderLine { Product = _context.Product.Find(ProdId), Quantity = Convert.ToInt32(qtyToAdd) });

                //save the cart session
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Index");
        }

        //remove item from the cart
        public IActionResult Remove(int id)
        {
            //read the cart items
            List<OrderLine> cart = SessionHelper.GetObjectFromJson<List<OrderLine>>(HttpContext.Session, "cart");
            //check if the product existed and remove it
            int index = isExist(id);
            cart.RemoveAt(index);
            //save the cart session
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
