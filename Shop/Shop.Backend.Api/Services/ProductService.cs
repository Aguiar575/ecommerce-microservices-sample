using System.Linq.Expressions;
using Shop.Backend.Api.Infrastructure;
using Shop.Backend.Api.Models;

namespace Shop.Backend.Api.Services;

public class ProductService : IProductService {
    private static bool IsDbAllowedToRunProcedures = true;
    private readonly ISnowflakeService _snowflakeService;
    private readonly IRepository<ProductModel> _productRepository;

    public ProductService(ISnowflakeService snowflakeService,
        IRepository<ProductModel> productRepository)
    {
        _snowflakeService = snowflakeService;
        _productRepository = productRepository;
    }

    public async Task<ProductModel?> CreateProduct(ProductModel input){
        SnowflakeIdViewModel snowflakeId = await _snowflakeService.SnowflakeId();
        input.Id = snowflakeId.Id.Value;

        ProductModel? product = await _productRepository.Insert(input);
        await _productRepository
            .SaveChangesWithIdentityInsertAsync(IsDbAllowedToRunProcedures);

        return product;
    }

    public async Task DeleteProduct(ulong id){
        _productRepository.Delete(id);
        await _productRepository.Save();
    }

    public async Task UpdateProduct(ProductModel UpdatedProduct) {
        ProductModel? actualProduct = 
            await _productRepository.GetByIDAsync(UpdatedProduct.Id);
        
        if(actualProduct != null){
            actualProduct.UpdateProductValues(UpdatedProduct);
            _productRepository.Update(actualProduct);
            await _productRepository.Save();
        }
    }

    public async Task<ProductModel?> GetProduct(ulong id) =>
        await _productRepository.GetByIDAsync(id);

    public async Task<IEnumerable<ProductModel>> GetProduct(
        Expression<Func<ProductModel, bool>>? filter = null)
    {
        return await _productRepository.Get(filter);
    }
}