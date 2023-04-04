﻿using AutoMapper;
using Mango.Services.ShoppingCartAPI.Models;
using Mango.Services.ShoppingCartAPI.Models.DTO;


namespace Mango.Services.ShoppingCartAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CartDTO, Cart>().ReverseMap();
                config.CreateMap<CartProductDTO, CartProduct>().ReverseMap();
                config.CreateMap<CartDetailsDTO, CartDetails>().ReverseMap();
                config.CreateMap<CartHeaderDTO, CartHeader>().ReverseMap();
            });

            return mappingConfig;
        }
    }
}
