using System.Linq.Expressions;
using AutoMapper;
using Shop.Backend.Api.Infrastructure;
using Shop.Backend.Api.Models;

namespace Shop.Backend.Api.Services;

public class ProductService : IProductService {
    private static bool IsDbAllowedToRunProcedures = true;
    private readonly ISnowflakeService _snowflakeService;
    private readonly IRepository<ProductModel> _productRepository;
    private readonly IMapper _mapper;

    public ProductService(ISnowflakeService snowflakeService,
        IRepository<ProductModel> productRepository,
        IMapper mapper)
    {
        _snowflakeService = snowflakeService;
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<ProductModel?> CreateProduct(ProductCreate input){
        ProductModel newProduct = _mapper.Map<ProductModel>(input);
        SnowflakeIdViewModel snowflakeId = await _snowflakeService.SnowflakeId();
        newProduct.Id = snowflakeId.Id.Value;

        ProductModel? product = await _productRepository.Insert(newProduct);
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