using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResturantAPI.Services.Dtos;
using ResturantAPI.Services.Enums;
using ResturantAPI.Services.IService;
using ResturantAPI.Services.Model;

namespace ResturantAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public DeliveryController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("GetAllDeliveries")]
        public async Task<Response<IEnumerable<OrderForDeliveryDto>>> GetAllOrdersForDelivery()
        {
            
                return await _orderService.GetAllOrdersForDeliveryAsync();
        }

        [HttpGet(" GetOrderDetailsForDelivery")]
        public async Task<IActionResult> GetOrderDetailsForDelivery(int id)
        {
            return Ok(await _orderService.GetOrderDetailsForDeliveryAsync(id));

        }

        ////UpdateOrderStatusAsync
        //[HttpPut("UpdateOrderStatus")]
        //public async Task<IActionResult> UpdateOrderStatus(int id, OrderStatus status)
        //{
        //    return Ok(await _orderService.UpdateOrderStatusAsync(id, status));
        //}


        [HttpPut("UpdateOrderStatusFromPindingtoOntheWay")]
        public async Task<IActionResult> UpdateOrderStatusFromPindingtoOntheWay(int id)
        {
            return Ok(await _orderService.UpdateOrderStatusFromPindingtoOntheWay(id));
        }
        [HttpPut("UpdateOrderStatusFromPindingtoDevlived")]
        public async Task<IActionResult> UpdateOrderStatusFromOntheWaytoDevlived(int id)
        {
            return Ok(await _orderService.UpdateOrderStatusFromOntheWaytoDevlived(id));
        }
        [HttpPut("UpdateOrderStatusFromPindingtoCancelled")]
        public async Task<IActionResult> UpdateOrderStatusFromDevlivedtoCancelled(int id)
        {
            return Ok(await _orderService.UpdateOrderStatusFromDevlivedtoCancelled(id));
        }

        //filter by order status
        [HttpPost("filter-by-status")]
        public async Task<IActionResult> GetOrdersByStatus([FromBody] OrderStatusFilterDto filterDto)
        {
            return Ok(await _orderService.GetOrdersByStatusAsync(filterDto.OrderStatus));
          
        }

        //GetOrdersByDeliveryAsync
        [HttpPost("by-delivery")]
        public async Task<IActionResult> GetOrdersByDelivery(int deliveryId) => Ok(await _orderService.GetOrdersByDeliveryAsync(deliveryId));






    }
}
