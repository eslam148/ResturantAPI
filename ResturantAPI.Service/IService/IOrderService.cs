using ResturantAPI.Services.Dtos;
using ResturantAPI.Services.Enums;
using ResturantAPI.Services.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResturantAPI.Services.IService
{
    public interface IOrderService
    {
        Task<Response<IEnumerable<OrderForDeliveryDto>>> GetAllOrdersForDeliveryAsync();
        Task<Response<OrderDetailsDto>> GetOrderDetailsForDeliveryAsync(int orderId);
        //UpdateOrderStatusAsync
        Task<Response<DeliveryStatusDto>> UpdateOrderStatusAsync(int orderId, OrderStatus status);
        Task<Response<DeliveryStatusDto>> UpdateOrderStatusFromPindingtoOntheWayAsync(int orderId);
        Task<Response<DeliveryStatusDto>> UpdateOrderStatusFromPindingtoDevlivedAsync(int orderId);
        Task<Response<DeliveryStatusDto>> UpdateOrderStatusFromPindingtoCancelledAsync(int orderId);
        Task<Response<IEnumerable<OrderStatusFilterDto>>> GetOrdersByStatusAsync(OrderStatus status);
        Task<Response<IEnumerable<OrdersByDeliveryDt>>> GetOrdersByDeliveryAsync(int deliveryId);


    }
}
