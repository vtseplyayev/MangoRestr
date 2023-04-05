using AutoMapper;
using Mango.Services.CouponAPI.Contexts;
using Mango.Services.CouponAPI.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Mango.Services.CouponAPI.Repository
{
    public class CouponRepository : ICouponRepository
    {
        private readonly ApplicationDBContext context;
        private IMapper mapper;

        public CouponRepository(ApplicationDBContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<CouponDTO> GetCouponByCode(string code)
        {
            var coupon = await context.Coupons.FirstOrDefaultAsync(u=>u.CouponCode == code);

            return mapper.Map<CouponDTO>(coupon);
        }
    }
}
