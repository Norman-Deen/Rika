using Microsoft.AspNetCore.Mvc;
using Rika_WebApp.Models;
using Rika_WebApp.ViewModels;
using System.Text.Json;

namespace Rika_WebApp.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            List<CartItemModel> cart;
            if (Request.Cookies.TryGetValue("Cart", out var cartJson))
            {
                cart = JsonSerializer.Deserialize<List<CartItemModel>>(cartJson) ?? [];
            }
            else
            {
                cart = [];
            }
            return View(cart);
        }


        [HttpPost]
        public IActionResult AddToCart([FromBody] ProductViewModel product)
        {
            List<CartItemModel> cart;
            if (Request.Cookies.TryGetValue("Cart", out var cartJson))
            {
                cart = JsonSerializer.Deserialize<List<CartItemModel>>(cartJson) ?? [];
            }
            else
            {
                cart = [];
            }

            var cartItem = cart.Find(item => item.ArticleNumber == product.ArticleNumber);
            if (cartItem != null)
            {
                cartItem.Quantity += product.Quantity;
            }
            else
            {
                var newCartItem = new CartItemModel
                {
                    ArticleNumber = product.ArticleNumber!,
                    ProductName = product.ProductName!,
                    Quantity = product.Quantity,
                    Price = product.Price
                };
                cart.Add(newCartItem);
            }

            var options = new CookieOptions { Expires = DateTimeOffset.UtcNow.AddDays(1) };
            Response.Cookies.Append("Cart", JsonSerializer.Serialize(cart), options);

            return Json(new { cartCount = cart.Sum(p => p.Quantity) });
        }

        [HttpPost]
        public IActionResult UpdateCartItem([FromBody] CartUpdateModel updateModel)
        {
            if (Request.Cookies.TryGetValue("Cart", out var cartJson))
            {
                var cart = JsonSerializer.Deserialize<List<CartItemModel>>(cartJson) ?? new List<CartItemModel>();

                var cartItem = cart.FirstOrDefault(item => item.ArticleNumber == updateModel.ArticleNumber);
                if (cartItem != null)
                {
                    if (updateModel.Quantity <= 0)
                    {
                        cart.Remove(cartItem);
                    }
                    else
                    {
                        cartItem.Quantity = updateModel.Quantity;
                    }
                    var options = new CookieOptions { Expires = DateTimeOffset.UtcNow.AddDays(1) };
                    Response.Cookies.Append("Cart", JsonSerializer.Serialize(cart), options);

                    if (cart.Count == 0)
                    {
                        return Json(new { redirectUrl = Url.Action("Index", "Cart") });
                    }
                    var totalQuantity = cart.Sum(p => p.Quantity);
                    var totalPrice = cart.Sum(p => p.Quantity * p.Price);

                    return Json(new { totalQuantity, totalPrice });
                }
            }
            return Json(new { error = "Item not found in cart" });
        }

        public IActionResult UpdateCartCount()
        {
            return ViewComponent("CartCount");
        }
    }
}