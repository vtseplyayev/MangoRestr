using Mango.Services.ShoppingCartAPI.Messaging;
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
        private readonly IMessageBus messageBus;
        protected ResponseDTO response;

        public CartController(ICartRepository cartRepository, IMessageBus messageBus)
        {
            this.cartRepository = cartRepository;
            this.messageBus = messageBus;
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

        [HttpPost("ApplyCoupon")]
        public async Task<object> ApplyCoupon([FromBody] CartDTO cartDTO)
        {
            try
            {
                bool isSuccess = await cartRepository.ApplyCoupon(cartDTO.CartHeader.UserId, cartDTO.CartHeader.CouponCode);
                response.IsSuccess = isSuccess;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string> { ex.ToString() };
            }

            return response;
        }

        [HttpPost("RemoveCoupon")]
        public async Task<object> RemoveCoupon([FromBody] string userId)
        {
            try
            {
                bool isSuccess = await cartRepository.RemoveCoupon(userId);
                response.IsSuccess = isSuccess;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string> { ex.ToString() };
            }

            return response;
        }

        [HttpPost("Checkout")]
        public async Task<object> Checkout(CheckoutHeaderDTO checkoutHeaderDTO)
        {
            try
            {
                CartDTO cartDTO = await cartRepository.GetCartByUserId(checkoutHeaderDTO.UserId);

                if(cartDTO == null)
                {
                    return BadRequest();
                }

                checkoutHeaderDTO.CartDetails = cartDTO.CartDetails;

                //logic
                await messageBus.PublishMessage(checkoutHeaderDTO, Config.CheckOutMessageTopic);
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
