using ResturantAPI.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResturantAPI.Services.IService
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderForDeliveryDto>> GetAllOrdersForDeliveryAsync();
        Task<OrderForDeliveryDto> GetOrderDetailsAsync(Guid orderId);
        Task<bool> AcceptOrderAsync(Guid orderId, Guid deliveryId);


    }
}
