using AutoMapper;
using Mango.Services.ShoppingCartAPI.Contexts;
using Mango.Services.ShoppingCartAPI.Models;
using Mango.Services.ShoppingCartAPI.Models.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
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
            var cartHeaderFromDb = await context.CartHeader.FirstOrDefaultAsync(u => u.UserId == userId);
            if (cartHeaderFromDb != null)
            {
                context.CartDetails
                    .RemoveRange(context.CartDetails.Where(u => u.CartHeaderId == cartHeaderFromDb.CartHeaderId));

                context.CartHeader.Remove(cartHeaderFromDb);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<CartDTO> CreateUpdateCart(CartDTO cartDTO)
        {
            Cart cart = mapper.Map<Cart>(cartDTO);

            var productInDb = await context.CartProduct
                .FirstOrDefaultAsync(u => u.ProductId == cartDTO.CartDetails.FirstOrDefault()
                .ProductId);

            if (productInDb == null)
            {
                context.CartProduct.Add(cart.CartDetails.FirstOrDefault().CartProduct);
                await context.SaveChangesAsync();
            }

            var cartHeaderFromDb = await context.CartHeader.AsNoTracking().FirstOrDefaultAsync(u => u.UserId == cart.CartHeader.UserId);

            if (cartHeaderFromDb == null)
            {
                context.CartHeader.Add(cart.CartHeader);
                await context.SaveChangesAsync();

                cart.CartDetails.FirstOrDefault().CartHeaderId = cart.CartHeader.CartHeaderId;
                cart.CartDetails.FirstOrDefault().CartProduct = null;
                context.CartDetails.Add(cart.CartDetails.FirstOrDefault());
                await context.SaveChangesAsync();
            }
            else
            {
                var cartDetailsFromDb = await context.CartDetails.AsNoTracking().FirstOrDefaultAsync(
                    u => u.ProductId == cart.CartDetails.FirstOrDefault().ProductId &&
                    u.CartHeaderId == cartHeaderFromDb.CartHeaderId);

                if (cartDetailsFromDb == null)
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
            Cart cart = new Cart()
            {
                CartHeader = await context.CartHeader.FirstOrDefaultAsync(u => u.UserId == userId)
            };

            cart.CartDetails = context.CartDetails
                .Where(u => u.CartHeaderId == cart.CartHeader.CartHeaderId)
                .Include(u => u.CartProduct);

            return mapper.Map<CartDTO>(cart);

        }

        public async Task<bool> RemoveFromCart(int cartDetailsId)
        {
            try
            {
                CartDetails cartDetails = await context.CartDetails
                    .FirstOrDefaultAsync(u => u.CartDetailsId == cartDetailsId);

                int totalCountOfCartItems = context.CartDetails
                      .Where(u => u.CartHeaderId == cartDetails.CartHeaderId).Count();

                context.CartDetails.Remove(cartDetails);

                if (totalCountOfCartItems == 1)
                {
                    var cartHeaderToRemove = await context.CartHeader
                        .FirstOrDefaultAsync(u => u.CartHeaderId == cartDetails.CartHeaderId);

                    context.CartHeader.Remove(cartHeaderToRemove);
                }

                await context.SaveChangesAsync();

                return true;

            } catch(Exception ex)
            {
                return false;
            }
        }
    }
}
