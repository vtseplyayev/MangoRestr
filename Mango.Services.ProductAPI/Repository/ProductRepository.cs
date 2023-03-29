using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Mango.Services.ProductAPI.Contexts;
using Mango.Services.ProductAPI.Models;
using Mango.Services.ProductAPI.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.ProductAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDBContext context;
        private IMapper mapper;

        public ProductRepository(ApplicationDBContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<ProductDTO> CreateUpdateProduct(ProductDTO productDTO)
        {
            Product product = mapper.Map<ProductDTO, Product>(productDTO);

            if (product.ProductId > 0)
                context.Products.Update(product);
            else
                context.Products.Add(product);

            await context.SaveChangesAsync();

            return mapper.Map<Product, ProductDTO>(product);
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            try
            {
                Product product = await context.Products.FirstOrDefaultAsync(u => u.ProductId == productId);

                if (product == null) return false;

                context.Products.Remove(product);
                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ProductDTO> GetProductById(int productId)
        {
            Product productList = await context.Products.Where(x => x.ProductId == productId).FirstOrDefaultAsync();
            return mapper.Map<ProductDTO>(productList);
        }

        public async Task<IEnumerable<ProductDTO>> GetProducts()
        {
            IEnumerable<Product> productList = await context.Products.ToListAsync();
            return mapper.Map<List<ProductDTO>>(productList);
        }
    }
}
