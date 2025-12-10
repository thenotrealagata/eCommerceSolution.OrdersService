namespace BusinessLogic.DTO
{
    public record OrderUpdateRequest (Guid OrderId, Guid UserId, DateTime OrderDate, List<OrderItemAddRequest> Items)
    {
        public OrderUpdateRequest() : this(default, default, default, default)
        {

        }
    }
}
