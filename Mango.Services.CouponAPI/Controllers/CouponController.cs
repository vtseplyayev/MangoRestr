using Mango.Services.CouponAPI.Models.DTO;
using Mango.Services.CouponAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Mango.Services.CouponAPI.Controllers
{
    [ApiController]
    [Route("api/coupon")]
    public class CouponController : Controller
    {
        private readonly ICouponRepository couponRepository;
        protected ResponseDTO response;

        public CouponController(ICouponRepository couponRepository)
        {
            this.couponRepository = couponRepository;
            this.response = new ResponseDTO();
        }


        [HttpGet("{code}")]
        public async Task<object> GetCoupon(string code)
        {
            try
            {
                CouponDTO cartDto = await couponRepository.GetCouponByCode(code);
                response.Result = cartDto;
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
