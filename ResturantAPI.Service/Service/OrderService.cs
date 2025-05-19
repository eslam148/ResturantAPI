using ResturantAPI.Domain.Interface;
using ResturantAPI.Services.Dtos;
using ResturantAPI.Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantAPI.Services.Service
{
    public class OrderService : IOrderService
    {

        private readonly IUnitOfWork _unitOfWork;
        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<IEnumerable<OrderForDeliveryDto>> GetAllOrdersForDeliveryAsync()
        {
            var orders = await _unitOfWork.OrderRepository.GetAllIncludingAsync(o => o.Customer, o => o.Delivery);
            return orders.Select(o => new OrderForDeliveryDto
            {
                OrderId = o.Id,
                CustomerName = o.Customer.Name,
                Address = o.Customer.Address.FullAddress,
                Status = o.Status.ToString(),
                DeliveryName = o.Delivery?.Name
            });
        }

        public Task<bool> AcceptOrderAsync(Guid orderId, Guid deliveryId)
        {
            throw new NotImplementedException();
        }
        public Task<OrderForDeliveryDto> GetOrderDetailsAsync(Guid orderId)
        {
            throw new NotImplementedException();
        }
    }
}
