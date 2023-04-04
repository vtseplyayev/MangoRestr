using AutoMapper;
using Mango.Services.ShoppingCartAPI.Contexts;
using Mango.Services.ShoppingCartAPI.Models;
using Mango.Services.ShoppingCartAPI.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Mango.Services.ShoppingCartAPI.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDBContext context;
        private IMapper mapper;

        public CartRepository(ApplicationDBContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<bool> CleanCart(string userId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<CartDTO> CreateUpdateCart(CartDTO cartDTO)
        {
            Cart cart = mapper.Map<Cart>(cartDTO);

            var productInDb = await context.CartProduct
                .FirstOrDefaultAsync(u => u.ProductId == cartDTO.CartDetails.FirstOrDefault()
                .ProductId);

            if(productInDb == null)
            {
                context.CartProduct.Add(cart.CartDetails.FirstOrDefault().CartProduct);
                await context.SaveChangesAsync();
            }

            var cartHeaderFromDb = await context.CartHeader.AsNoTracking().FirstOrDefaultAsync(u => u.UserId == cart.CartHeader.UserId);

            if(cartHeaderFromDb == null)
            {
                context.CartHeader.Add(cart.CartHeader);
                await context.SaveChangesAsync();

                cart.CartDetails.FirstOrDefault().CartHeaderId = cart.CartHeader.CartHeaderId;
                cart.CartDetails.FirstOrDefault().CartProduct = null;
                context.CartDetails.Add(cart.CartDetails.FirstOrDefault());
                await context.SaveChangesAsync();
            } else
            {
                var cartDetailsFromDb = await context.CartDetails.AsNoTracking().FirstOrDefaultAsync(
                    u => u.ProductId == cart.CartDetails.FirstOrDefault().ProductId &&
                    u.CartHeaderId == cartHeaderFromDb.CartHeaderId);

                if(cartDetailsFromDb == null)
                {
                    cart.CartDetails.FirstOrDefault().CartHeaderId = cartHeaderFromDb.CartHeaderId;
                    cart.CartDetails.FirstOrDefault().CartProduct = null;
                    context.CartDetails.Add(cart.CartDetails.FirstOrDefault());
                    await context.SaveChangesAsync();
                } 
                else
                {
                    cart.CartDetails.FirstOrDefault().CartProduct = null;
                    cart.CartDetails.FirstOrDefault().Count += cartDetailsFromDb.Count;
                    context.CartDetails.Update(cart.CartDetails.FirstOrDefault());
                    await context.SaveChangesAsync();
                }
            }

            return mapper.Map<CartDTO>(cart);
        }

        public async Task<CartDTO> GetCartByUserId(string userId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> RemoveFromCart(int cartDetailsId)
        {
            throw new System.NotImplementedException();
        }
    }
}
