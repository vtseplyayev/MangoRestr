using Mango.Services.ShoppingCartAPI.Models.DTO;
using Mango.Services.ShoppingCartAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mango.Services.ShoppingCartAPI.Controllers
{
    [ApiController]
    [Route("api/cart")]
    public class CartController : Controller
    {
        private readonly ICartRepository cartRepository;
        protected ResponseDTO response;

        public CartController(ICartRepository cartRepository)
        {
            this.cartRepository = cartRepository;
            this.response = new ResponseDTO();
        }

        [HttpGet("GetCart/{userId}")]
        public async Task<object> GetCart(string userId)
        {
            try
            {
                CartDTO cartDto = await cartRepository.GetCartByUserId(userId);
                response.Result = cartDto;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string> { ex.ToString() };
            }

            return response;
        }

        [HttpPost("AddCart")]
        public async Task<object> AddCart(CartDTO cart)
        {
            try
            {
                CartDTO cartDto = await cartRepository.CreateUpdateCart(cart);
                response.Result = cartDto;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string> { ex.ToString() };
            }

            return response;
        }

        [HttpPost("UpdateCart")]
        public async Task<object> UpdateCart(CartDTO cart)
        {
            try
            {
                CartDTO cartDto = await cartRepository.CreateUpdateCart(cart);
                response.Result = cartDto;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string> { ex.ToString() };
            }

            return response;
        }

        [HttpPost("RemoveCart")]
        public async Task<object> RemoveCart([FromBody]int cartId)
        {
            try
            {
                bool isSuccess = await cartRepository.RemoveFromCart(cartId);
                response.IsSuccess = isSuccess;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string> { ex.ToString() };
            }

            return response;
        }
    }
}
