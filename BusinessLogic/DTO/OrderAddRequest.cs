namespace BusinessLogic.DTO
{
    public record OrderAddRequest (Guid UserId, List<OrderItemAddRequest> Items)
    {
        public OrderAddRequest() : this(default, new List<OrderItemAddRequest>()) {

        }
    }
}
