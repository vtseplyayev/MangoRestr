using AutoMapper;
//using Mango.Services.ProductAPI.Models.DTO;
//using Mango.Services.ProductAPI.Models;

namespace Mango.Services.ShoppingCartAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                //config.CreateMap<ProductDTO, Product>();
                //config.CreateMap<Product, ProductDTO>();
            });

            return mappingConfig;
        }
    }
}
