using MongoDB.Bson.Serialization.Attributes;

namespace DataAccess.Entities
{
    public class Order
    {
        [BsonId]
        public Guid _id { get; set; }

        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public Guid OrderId { get; set; }

        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public Guid UserId { get; set; }

        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public DateTime OrderDate { get; set; }

        [BsonRepresentation(MongoDB.Bson.BsonType.Decimal128)]
        public decimal TotalBill { get; set; }

        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}
