using AutoMapper;
using Shop.Backend.Api.Models;

namespace Infrastructure.Mapper;

public class ProductProfile : Profile {
    public ProductProfile() =>
        CreateMap<ProductCreate, ProductModel>();
}