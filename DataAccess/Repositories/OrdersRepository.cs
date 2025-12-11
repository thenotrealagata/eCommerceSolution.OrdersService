using DataAccess.Entities;
using DataAccess.RepositoryContracts;
using MongoDB.Driver;

namespace DataAccess.Repositories
{
    internal class OrdersRepository : IOrdersRepository
    {
        private readonly string _collectionName = "orders";
        private readonly IMongoCollection<Order> _orders;

        public OrdersRepository(IMongoDatabase mongoDatabase) {
            _orders = mongoDatabase.GetCollection<Order>(_collectionName);
        }

        public async Task<IEnumerable<Order>> GetOrders()
        {
            FilterDefinition<Order> filter = Builders<Order>.Filter.Empty;

            return (await _orders.FindAsync(filter)).ToList();
        }

        public async Task<Order?> GetOrderByCondition(FilterDefinition<Order> filter)
        {
            return (await _orders.FindAsync(filter)).FirstOrDefault();
        }

        public async Task<List<Order>> GetOrdersByCondition(FilterDefinition<Order> filter)
        {
            return await (await _orders.FindAsync(filter)).ToListAsync();
        }

        public async Task<Order?> CreateOrder(Order order)
        {
            order.OrderId = Guid.NewGuid();
            order._id = order.OrderId;

            foreach (OrderItem orderItem in order.Items) {
                orderItem._id = Guid.NewGuid();
            }

            await _orders.InsertOneAsync(order);

            return order;
        }

        public async Task<Order?> UpdateOrder(Order order)
        {
            FilterDefinition<Order> filter = Builders<Order>.Filter.Eq(o => o.OrderId, order.OrderId);

            Order? existingOrder = (await _orders.FindAsync(filter)).FirstOrDefault();

            if (existingOrder is null)
            {
                return null;
            }

            ReplaceOneResult updateResult = await _orders.ReplaceOneAsync(filter, order);
            return updateResult.ModifiedCount > 0 ? order : null;
        }

        public async Task<bool> DeleteOrder(Guid orderId)
        {
            FilterDefinition<Order> filter = Builders<Order>.Filter.Eq(o => o.OrderId, orderId);

            Order? existingOrder = (await _orders.FindAsync(filter)).FirstOrDefault();

            if (existingOrder is null)
            { 
                return false;
            }

            DeleteResult deleteResult = await _orders.DeleteOneAsync(filter);
            return deleteResult.DeletedCount > 0;
        }
    }
}
