using AutoMapper;
using ShoppingStore.API.DTO.CategoryDTo;
using ShoppingStore.API.DTO.OrderDTos;
using ShoppingStore.API.DTO.ProductDtos;
using ShoppingStore.Repository.Models;

namespace ShoppingStore.API.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDTo>().ReverseMap();
            CreateMap<Category, CategoryUpdateDto>().ReverseMap();

            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, ProductUpdateDto>().ReverseMap();

            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<Order, OrderUpdateDto>().ReverseMap();
        }
    }
}
