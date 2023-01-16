namespace Shop.Backend.Api.Dto;

public record struct SnowflakeIdRequest (ulong? Id, bool IsSuccess = true);