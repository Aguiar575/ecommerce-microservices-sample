namespace Shop.Backend.Api.Dto;

public record struct ProductUpdate(ulong Id, int Price, string Name);