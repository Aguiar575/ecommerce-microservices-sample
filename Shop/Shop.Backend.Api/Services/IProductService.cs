using System.Linq.Expressions;
using Shop.Backend.Api.Dto;
using Shop.Backend.Api.Models;

namespace Shop.Backend.Api.Services;

public interface IProductService {
    Task<ProductModel?> CreateProduct(ProductCreate product);
    Task DeleteProduct(ulong id);
    Task UpdateProduct(ProductUpdate productUpdate);
    Task<ProductModel?> GetProduct(ulong id);
    Task<IEnumerable<ProductModel>> GetProduct(
        Expression<Func<ProductModel, bool>>? filter = null);
}