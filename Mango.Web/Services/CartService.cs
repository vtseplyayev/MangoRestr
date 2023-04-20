using Mango.Web.Models;
using Mango.Web.Services.IServices;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mango.Web.Services
{
    public class CartService : BaseService, ICartService
    {
        private readonly IHttpClientFactory clientFactory;

        public CartService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            this.clientFactory = clientFactory;
        }

        public async Task<T> AddToCartAsync<T>(CartDTO cartDto, string token = null)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = Config.ApiType.POST,
                Data = cartDto,
                Url = Config.ShoppingCartAPIBase + "/api/cart/addcart",
                AccessToken = token
            });
        }

        public async Task<T> ApplyCoupon<T>(CartDTO cartDto, string token = null)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = Config.ApiType.POST,
                Data = cartDto,
                Url = Config.ShoppingCartAPIBase + "/api/cart/applycoupon",
                AccessToken = token
            });
        }

        public async Task<T> Checkout<T>(CartHeaderDTO cartHeader, string token = null)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = Config.ApiType.POST,
                Data = cartHeader,
                Url = Config.ShoppingCartAPIBase + "/api/cart/checkout",
                AccessToken = token
            });
        }

        public async Task<T> GetCartByUserIdAsync<T>(string userId, string token = null)
        {
            return await SendAsync<T>(new ApiRequest()
            {
                ApiType = Config.ApiType.GET,
                Url = Config.ShoppingCartAPIBase + "/api/cart/getcart/" + userId,
                AccessToken = token
            });
        }

        public async Task<T> RemoveCoupon<T>(string userId, string token = null)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = Config.ApiType.POST,
                Data = userId,
                Url = Config.ShoppingCartAPIBase + "/api/cart/removecoupon",
                AccessToken = token
            });
        }

        public async Task<T> RemoveFromCartAsync<T>(int cartId, string token = null)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = Config.ApiType.POST,
                Data = cartId,
                Url = Config.ShoppingCartAPIBase + "/api/cart/removecart",
                AccessToken = token
            });
        }

        public async Task<T> UpdateCartAsync<T>(CartDTO cartDto, string token = null)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = Config.ApiType.POST,
                Data = cartDto,
                Url = Config.ShoppingCartAPIBase + "/api/cart/updatecart",
                AccessToken = token
            });
        }
    }
}
