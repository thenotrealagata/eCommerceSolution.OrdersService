using AutoMapper;
using BusinessLogic.DTO;
using DataAccess.Entities;

namespace BusinessLogic.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Order requests
            CreateMap<OrderAddRequest, Order>();
            CreateMap<OrderUpdateRequest, Order>();

            // Order item requests
            CreateMap<OrderItemAddRequest, OrderItem>();
            CreateMap<OrderItemUpdateRequest, OrderItem>();

            // Order and order item responses
            CreateMap<Order, OrderResponse>();
            CreateMap<OrderItem, OrderItemResponse>();
        }
    }
}
