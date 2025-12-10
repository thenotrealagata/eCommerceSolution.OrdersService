using AutoMapper;
using BusinessLogic.DTO;
using BusinessLogic.ServiceContracts;
using DataAccess.Entities;
using DataAccess.RepositoryContracts;
using MongoDB.Driver;

namespace BusinessLogic.Services
{
    internal class OrdersService : IOrdersService
    {
        private readonly IMapper _mapper;
        private readonly IOrdersRepository _ordersRepository;

        public OrdersService(IMapper mapper, IOrdersRepository ordersRepository)
        {
            _mapper = mapper;
            _ordersRepository = ordersRepository;
        }

        public Task<List<OrderResponse>> GetOrders()
        {
            throw new NotImplementedException();
        }

        public Task<OrderResponse?> GetOrderByCondition(FilterDefinition<Order> filter)
        {
            throw new NotImplementedException();
        }

        public Task<List<OrderResponse>> GetOrdersByCondition(FilterDefinition<Order> filter)
        {
            throw new NotImplementedException();
        }

        public Task<OrderResponse?> AddOrder(OrderAddRequest orderAddRequest)
        {
            throw new NotImplementedException();
        }

        public Task<OrderResponse?> UpdateOrder(OrderUpdateRequest orderUpdateRequest)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteOrder(Guid orderId)
        {
            throw new NotImplementedException();
        }
    }
}
