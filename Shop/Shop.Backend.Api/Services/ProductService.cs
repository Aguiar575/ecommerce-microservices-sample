using Shop.Backend.Api.Infrastructure;
using Shop.Backend.Api.Models;

namespace Shop.Backend.Api.Services;

public class ProductService {
    private static bool IsDbAllowedToRunProcedures = true;
    private readonly ISnowflakeService _snowflakeService;
    private readonly IRepository<ProductModel> _productRepository;

    public ProductService(ISnowflakeService snowflakeService,
        IRepository<ProductModel> productRepository)
    {
        _snowflakeService = snowflakeService;
        _productRepository = productRepository;
    }

    public async Task CreateProduct(ProductModel product){
        await _productRepository.Insert(product);
        await _productRepository
            .SaveChangesWithIdentityInsertAsync(IsDbAllowedToRunProcedures);
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
}