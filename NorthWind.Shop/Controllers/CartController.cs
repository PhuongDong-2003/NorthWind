using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NorthWind.Core.Entity;
using NorthWind.Shop.Models;
using NorthWind.Shop.Service;
using System.Web;
using NorthWind.Web.Service;
namespace NorthWind.Shop.Controllers
{
    public class CartController : Controller
    {

        private readonly CartService _cartService;
        private readonly CustomerService _customerService;

        private readonly OrderService _orderService;
     

        public CartController(CartService cartService, CustomerService customerService, OrderService orderService)
        {
            _cartService = cartService;
            _customerService = customerService;
            _orderService = orderService;
         
        }
        [HttpGet]
        public IActionResult Cart()
        {
            decimal ToTal = 0;
            Dictionary<int, CartItem> cart = GetCartFromCookie();

            foreach (var i in cart)
            {
                ToTal += i.Value.Quantity * i.Value.UnitPrice;
            }
            ViewBag.ToTal = ToTal;
            ViewBag.Cart = cart;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCart(int productId)
        {

            var response = await _cartService.GetByID(productId);

            Dictionary<int, CartItem> cart = GetCartFromCookie();

            if (cart.ContainsKey(productId))
            {
                cart[productId].Quantity++;
            }
            else
            {
                cart[productId] = new CartItem
                {
                    ProductID = response.ProductID,
                    ProductName = response.ProductName,
                    UnitPrice = (decimal)response.UnitPrice,
                    Quantity = 1,
                };
            }
            SaveCartToCookie(cart);
            return Json(new { success = true });
        }
        private Dictionary<int, CartItem> GetCartFromCookie()
        {
            string cartJson = HttpContext.Request.Cookies["cart"];

            if (!string.IsNullOrEmpty(cartJson))
            {
                return JsonSerializer.Deserialize<Dictionary<int, CartItem>>(cartJson);
            }

            return new Dictionary<int, CartItem>();
        }
        private void SaveCartToCookie(Dictionary<int, CartItem> cart)
        {
            var cartJson = JsonSerializer.Serialize(cart);

            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(20)
            };

            HttpContext.Response.Cookies.Append("cart", cartJson, cookieOptions);
        }

        [HttpPost]
        public IActionResult UpdateCart(int productId, int quantity)
        {
            Dictionary<int, CartItem> cart = GetCartFromCookie();

            if (cart.ContainsKey(productId))
            {
                if (quantity > 0)
                {
                    cart[productId].Quantity = quantity;
                }
                else
                {                  
                    cart.Remove(productId);
                }

                SaveCartToCookie(cart);
            }

            return Redirect("Cart");
        }

        [HttpPost]
        public IActionResult DeleteCart(int productId)
        {
            Dictionary<int, CartItem> cart = GetCartFromCookie();

            if (cart.ContainsKey(productId))
            {
                cart.Remove(productId);
                SaveCartToCookie(cart);
            }

            return RedirectToAction("Cart", "Cart");
        }
        public void Clear()
        {      
                Response.Cookies.Delete("cart");
                
        }
        
        [HttpPost]
        public async Task<IActionResult> CheckOut()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var nameac = HttpContext.User.Identity.Name;
                Customer customer = await _customerService.GetCustomerByID(nameac);

                Dictionary<int, CartItem> cart = GetCartFromCookie();

                Order order = new Order
                {
                    CustomerID = customer.CustomerID,
                    EmployeeID = null,
                    OrderDate = DateTime.Now,
                    RequiredDate = DateTime.Now,
                    ShippedDate = DateTime.Now,
                    ShipVia = null,
                    Freight = 89,
                    ShipName = "D",
                    ShipCity = "D",
                    ShipRegion = "D",
                    ShipPostalCode = "5008",
                    ShipCountry = "D",
                    OrderDetails = new List<OrderDetail>() 
                };

                foreach (var i in cart)
                {
                    OrderDetail orderDetail = new OrderDetail
                    {
                        ProductID = i.Value.ProductID,
                        UnitPrice = i.Value.UnitPrice,
                        Quantity = (short)i.Value.Quantity,
                        Discount = 0
                    };

                    order.OrderDetails.Add(orderDetail);  
                }

                await _orderService.InsertOrder(order);
                Clear();

                return Redirect("Cart");
            }
            else
            {
                return RedirectToAction("Index", "Account");
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }

    }
}
