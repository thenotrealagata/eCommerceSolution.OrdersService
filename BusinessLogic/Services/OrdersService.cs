using AutoMapper;
using BusinessLogic.DTO;
using BusinessLogic.HttpClients;
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
        private readonly UsersMicroserviceClient _usersMicroserviceClient;

        public OrdersService(IMapper mapper, IOrdersRepository ordersRepository, UsersMicroserviceClient usersMicroserviceClient)
        {
            _mapper = mapper;
            _ordersRepository = ordersRepository;
            _usersMicroserviceClient = usersMicroserviceClient;
        }

        public async Task<List<OrderResponse>> GetOrders()
        {
            IEnumerable<Order> orders = (await _ordersRepository.GetOrders());
            List<OrderResponse> ordersResponse = orders.Select(o => _mapper.Map<OrderResponse>(o)).ToList();

            return ordersResponse;
        }

        public async Task<OrderResponse?> GetOrderByCondition(FilterDefinition<Order> filter)
        {
            Order? order = await _ordersRepository.GetOrderByCondition(filter);

            if (order is null)
            {
                return null;
            }

            return _mapper.Map<OrderResponse>(order);
        }

        public async Task<List<OrderResponse>> GetOrdersByCondition(FilterDefinition<Order> filter)
        {
            List<Order> orders = await _ordersRepository.GetOrdersByCondition(filter);

            return orders.Select(order => _mapper.Map<OrderResponse>(order)).ToList();
        }

        public async Task<OrderResponse?> AddOrder(OrderAddRequest orderAddRequest)
        {
            Guid userId = orderAddRequest.UserId;
            UserResponse? response = await _usersMicroserviceClient.GetUserById(userId);

            if (response is null)
            {
                throw new ArgumentException("User ID is invalid!");
            }

            Order orderInput = _mapper.Map<Order>(orderAddRequest);

            foreach (OrderItem orderItem in orderInput.Items)
            {
                orderItem.TotalPrice = orderItem.Quantity * orderItem.UnitPrice;
            }
            orderInput.TotalBill = orderInput.Items.Sum(temp => temp.TotalPrice);

            Order? addedOrder = await _ordersRepository.CreateOrder(orderInput);

            if (addedOrder == null)
            {
                return null;
            }

            OrderResponse addedOrderResponse = _mapper.Map<OrderResponse>(addedOrder);

            return addedOrderResponse;
        }

        public async Task<OrderResponse?> UpdateOrder(OrderUpdateRequest orderUpdateRequest)
        {
            Guid userId = orderUpdateRequest.UserId;
            UserResponse? response = await _usersMicroserviceClient.GetUserById(userId);

            if (response is null)
            {
                throw new ArgumentException("User ID is invalid!");
            }

            Order orderInput = _mapper.Map<Order>(orderUpdateRequest);

            foreach (OrderItem orderItem in orderInput.Items)
            {
                orderItem.TotalPrice = orderItem.Quantity * orderItem.UnitPrice;
            }
            orderInput.TotalBill = orderInput.Items.Sum(temp => temp.TotalPrice);

            Order? updatedOrder = await _ordersRepository.UpdateOrder(orderInput);

            if (updatedOrder == null)
            {
                return null;
            }

            OrderResponse addedOrderResponse = _mapper.Map<OrderResponse>(updatedOrder);

            return addedOrderResponse;
        }

        public async Task<bool> DeleteOrder(Guid orderId)
        {
            bool success = await _ordersRepository.DeleteOrder(orderId);

            return success;
        }
    }
}
