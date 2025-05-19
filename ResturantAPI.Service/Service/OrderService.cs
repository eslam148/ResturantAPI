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
            var orders = await _unitOfWork.OrderRepository.GetAllIncludingAsync(o => o.Customer.user, o => o.Delivery.User);
            return orders.Select(o => new OrderForDeliveryDto
            {
                OrderId = o.Id,
                CustomerName = o.Customer.user.Name,
                Address = o.Customer.Addresses.FirstOrDefault().Country,
                Status = o.Status.ToString(),
                DeliveryName = o.Delivery?.User.Name
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
