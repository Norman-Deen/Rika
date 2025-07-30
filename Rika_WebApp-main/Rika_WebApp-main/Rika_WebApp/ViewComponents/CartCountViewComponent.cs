using Microsoft.AspNetCore.Mvc;
using Rika_WebApp.Models;
using Rika_WebApp.ViewModels;
using System.Text.Json;

namespace Rika_WebApp.ViewComponents
{
    public class CartCountViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            List<CartItemModel> cart;
            if (Request.Cookies.TryGetValue("Cart", out string? cartJson))
            {
                cart = JsonSerializer.Deserialize<List<CartItemModel>>(cartJson) ?? [];
            }
            else
            {
                cart = [];
            }
            return View(cart);
        }
    }
}