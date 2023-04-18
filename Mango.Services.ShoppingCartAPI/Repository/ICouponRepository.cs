using Mango.Services.ShoppingCartAPI.Models.DTO;
using System.Threading.Tasks;

namespace Mango.Services.ShoppingCartAPI.Repository
{
    public interface ICouponRepository
    {
        Task<CouponDTO> GetCoupon(string couponName);
    }
}
