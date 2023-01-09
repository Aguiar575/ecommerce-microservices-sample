using ShopBackend.Infrastructure;
using ShopBackend.Models;

namespace ShopBackend.Services;

public class ProductService {
    private readonly ISnowflakeService _snowflakeService;
    private readonly IRepository<ProductModel> _productRepository;

    public ProductService(ISnowflakeService snowflakeService,
        IRepository<ProductModel> productRepository)
    {
        _snowflakeService = snowflakeService;
        _productRepository = productRepository;
    }
}