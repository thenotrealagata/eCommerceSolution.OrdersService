namespace BusinessLogic.DTO
{
    public record OrderAddRequest (Guid UserId, DateTime OrderDate, List<OrderItemAddRequest> Items)
    {
        public OrderAddRequest() : this(default, default, default) {

        }
    }
}
