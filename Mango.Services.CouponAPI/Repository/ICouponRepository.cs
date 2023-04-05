using Mango.Services.CouponAPI.Models.DTO;
using System.Threading.Tasks;

namespace Mango.Services.CouponAPI.Repository
{
    public interface ICouponRepository
    {
        Task<CouponDTO> GetCouponByCode(string code);
    }
}
