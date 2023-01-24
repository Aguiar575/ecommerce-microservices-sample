using Shop.Backend.Api.Dto;

namespace Shop.Backend.Api.Services;

public interface ISnowflakeService {
    Task<SnowflakeIdRequest> SnowflakeId();   
}