using BusinessLogic.DTO;
using DataAccess.Entities;
using MongoDB.Driver;

namespace BusinessLogic.ServiceContracts
{
    public interface IOrdersService
    {
        Task<List<OrderResponse>> GetOrders();
        Task<OrderResponse?> GetOrderByCondition(FilterDefinition<Order> filter);
        Task<List<OrderResponse>> GetOrdersByCondition(FilterDefinition<Order> filter);
        Task<OrderResponse?> AddOrder(OrderAddRequest orderAddRequest);
        Task<OrderResponse?> UpdateOrder(OrderUpdateRequest orderUpdateRequest);
        Task<bool> DeleteOrder(Guid orderId);
    }
}
