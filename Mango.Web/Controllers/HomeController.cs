using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Mango.Web.Models;
using Mango.Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Mango.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly IProductService productService;
        private readonly ICartService cartService;

        public HomeController(ILogger<HomeController> logger, IProductService productService, ICartService cartService)
        {
            this.logger = logger;
            this.productService = productService;
            this.cartService = cartService;
        }

        public async Task<IActionResult> Index()
        {
            List<ProductDTO> list = new();

            var response = await productService.GetAllProductAsync<ResponseDTO>("");

            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ProductDTO>>(Convert.ToString(response.Result));
            }

            return View(list);
        }

        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            ProductDTO model = new();

            var response = await productService.GetProductByIdAsync<ResponseDTO>(id, "");

            if (response != null && response.IsSuccess)
            {
                model = JsonConvert.DeserializeObject<ProductDTO>(Convert.ToString(response.Result));
            }

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Details(ProductDTO productDTO)
        {
            CartDTO cartDto = new()
            {
                CartHeader = new CartHeaderDTO
                {
                    UserId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value
                }
            };

            CartDetailsDTO cartDetails = new()
            {
                Count = productDTO.Count,
                ProductId = productDTO.ProductId
            };

            var response = await productService.GetProductByIdAsync<ResponseDTO>(productDTO.ProductId, "");
            if(response != null && response.IsSuccess)
            {
                cartDetails.CartProduct = JsonConvert.DeserializeObject<ProductDTO>(Convert.ToString(response.Result));
            }

            List<CartDetailsDTO> cartDetailsDtos = new ();
            cartDetailsDtos.Add(cartDetails);

            cartDto.CartDetails = cartDetailsDtos;

            var accessTokem = await HttpContext.GetTokenAsync("access_token");
            var addToCartResponse = await cartService.AddToCartAsync<ResponseDTO>(cartDto, accessTokem);

            if (addToCartResponse != null && addToCartResponse.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(productDTO);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize]
        public async Task<IActionResult> Login()
        {
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Logout()
        {
            return SignOut("Cookies", "oidc");
        }
    }
}
