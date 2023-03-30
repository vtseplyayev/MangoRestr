﻿using Mango.Web.Models;
using System.Threading.Tasks;

namespace Mango.Web.Services.IServices
{
    public interface IProductService
    {
        Task<T> GetAllProductAsync<T>(string token);
        Task<T> GetProductByIdAsync<T>(int id, string token);
        Task<T> CreateProductAsync<T>(ProductDTO productDTO, string token);
        Task<T> UpdateProductAsync<T>(ProductDTO productDTO, string token);
        Task<T> DeleteProductAsync<T>(int id, string token);
    }
}
