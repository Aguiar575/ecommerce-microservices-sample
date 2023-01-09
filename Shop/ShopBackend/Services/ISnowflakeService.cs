using ShopBackend.Models;

namespace ShopBackend.Services;

public interface ISnowflakeService {
    Task<SnowflakeIdViewModel> SnowflakeId();   
}