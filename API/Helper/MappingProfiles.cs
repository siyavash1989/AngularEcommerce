using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;
using Core.Entities.OrderAggregate;

namespace API.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Title))
                .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Title))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>());

            CreateMap<Core.Entities.Identity.Address, AddressDto>().ReverseMap();
            CreateMap<CustomerBasket, CustomerBasketDto>().ReverseMap();
            CreateMap<CustomerBasketItem, CustomerBasketItemDto>().ReverseMap();
            CreateMap<AddressDto, Core.Entities.OrderAggregate.Address>();

            CreateMap<Order, OrderToReturnDto>().ReverseMap();
            CreateMap<OrderItem, OrderItemDto>().ReverseMap();

        }
    }
}