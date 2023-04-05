using Mango.Web.Models;
using Mango.Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Mango.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductService productService;
        private readonly ICartService cartService;

        public CartController(IProductService productService, ICartService cartService)
        {
            this.productService = productService;
            this.cartService = cartService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await LoadCartDtoBasedOnLoggedInUser());
        }

        public async Task<IActionResult> Remove(int cartId)
        {
            var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await cartService.RemoveFromCartAsync<ResponseDTO>(cartId, accessToken);

            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        private async Task<CartDTO> LoadCartDtoBasedOnLoggedInUser()
        {
            var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await cartService.GetCartByUserIdAsync<ResponseDTO>(userId, accessToken);

            CartDTO cartDto = new();

            if(response != null && response.IsSuccess) 
            {
                cartDto = JsonConvert.DeserializeObject<CartDTO>(Convert.ToString(response.Result));
            }

            if(cartDto.CartHeader != null)
            {
                foreach (var item in cartDto.CartDetails)
                {
                    cartDto.CartHeader.OrderTotal += (item.CartProduct.Price * item.Count);
                }
            }

            return cartDto;
        }
    }
}
