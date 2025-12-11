using BusinessLogic.DTO;
using BusinessLogic.ServiceContracts;
using DataAccess.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace eCommerce.OrdersService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersService _ordersService;

        public OrdersController(IOrdersService ordersService)
        {
            _ordersService = ordersService;
        }

        [HttpGet]
        public async Task<List<OrderResponse>> GetOrders()
        {
            List<OrderResponse> orders = await _ordersService.GetOrders();

            return orders;
        }

        [HttpGet("/{orderId}")]
        public async Task<OrderResponse?> GetOrderById([FromQuery] Guid orderId)
        {
            FilterDefinition<Order> filter = Builders<Order>.Filter.Eq(o => o.OrderId, orderId);
            OrderResponse? order = await _ordersService.GetOrderByCondition(filter);

            return order;
        }

        [HttpPost]
        public async Task<ActionResult<OrderResponse?>> CreateOrder([FromBody] OrderAddRequest orderAddRequest)
        {
            OrderResponse? createdOrder = await _ordersService.AddOrder(orderAddRequest);

            if (createdOrder is null)
            {
                return Problem("Error creating order.");
            }

            return CreatedAtAction(nameof(GetOrderById), new { orderId = createdOrder.OrderId }, createdOrder);
        }


        [HttpPut]
        public async Task<OrderResponse?> UpdateOrder([FromBody] OrderUpdateRequest orderUpdateRequest)
        {
            OrderResponse? updatedOrder = await _ordersService.UpdateOrder(orderUpdateRequest);

            return updatedOrder;
        }

        [HttpDelete("/{orderId}")]
        public async Task<IActionResult> DeleteOrder([FromQuery] Guid orderId)
        {
            if (orderId == Guid.Empty)
            {
                return BadRequest("OrderId can't be empty.");
            }

            bool isDeleted = await _ordersService.DeleteOrder(orderId);

            if (!isDeleted)
            {
                return Problem("Error deleting product.");
            }

            return Ok();
        }
    }
}
