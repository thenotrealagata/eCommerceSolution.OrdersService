using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTO
{
    public record OrderResponse (Guid OrderId, Guid UserId, DateTime OrderDate, List<OrderItemResponse> Items)
    {
        public OrderResponse() : this(default, default, default, default)
        {

        }
    }
}
