using Shop.Backend.Api.Models;

namespace Shop.Backend.Api.Services;

public interface ISnowflakeService {
    Task<SnowflakeIdViewModel> SnowflakeId();   
}