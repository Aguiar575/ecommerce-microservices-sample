using System.Linq.Expressions;
using Shop.Backend.Api.Models;

namespace Shop.Backend.Api.Services;

public interface IProductService {
    Task<ProductModel?> CreateProduct(ProductModel product);
    Task DeleteProduct(ulong id);
    Task UpdateProduct(ProductModel UpdatedProduct);
    Task<ProductModel?> GetProduct(ulong id);
    Task<IEnumerable<ProductModel>> GetProduct(
        Expression<Func<ProductModel, bool>>? filter = null);
}