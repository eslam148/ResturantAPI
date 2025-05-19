using ResturantAPI.Domain.Entities;
using ResturantAPI.Domain.Interface;
using ResturantAPI.Services.Dtos;
using ResturantAPI.Services.Enums;
using ResturantAPI.Services.IService;
using ResturantAPI.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using t7kmplus.Service.Dtos;
using static ResturantAPI.Services.Dtos.OrderDetailsDto;

namespace ResturantAPI.Services.Service
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<IEnumerable<OrderForDeliveryDto>>> GetAllOrdersForDeliveryAsync()
        {
            try
            {
                var orders = _unitOfWork.OrderRepository.FilterAll(
                               e => e.Status == Domain.Entities.OrderStatus.Pending,
                               include: ["Customer.user", "DeliveryAddress", "Delivery.User"], true
                           );

                var x = orders.Select(o => new OrderForDeliveryDto
                {
                    OrderId = o.Id,
                    CustomerName = o.Customer.user.Name,
                    Address = $"{o.DeliveryAddress.Street}, {o.DeliveryAddress.City}, {o.DeliveryAddress.Country}",
                    Status = o.Status.ToString(),
                    DeliveryName = o.Delivery.User.Name
                });
                return new Response<IEnumerable<OrderForDeliveryDto>>
                {
                    Data = x,
                    Status = ResponseStatus.Success,
                    Message = "Orders retrieved successfully",
                    InternalMessage = "Orders retrieved successfully",

                };

            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<OrderForDeliveryDto>>
                {
                    Status = ResponseStatus.InternalServerError,
                    Message = "An error occurred while retrieving orders",
                    InternalMessage = ex.Message
                };
            }

        }


        public async Task<Response<OrderDetailsDto>> GetOrderDetailsForDeliveryAsync(int orderId)
        {
            try
            {
                var order = await _unitOfWork.OrderRepository.GetByIdAsync(
                    orderId,
                    include: ["Customer.user", "DeliveryAddress", "Delivery.User", "Items.MenuItem"], true
                );

                if (order == null)
                {
                    return new Response<OrderDetailsDto>
                    {
                        Status = ResponseStatus.NotFound,
                        Message = "Order not found",
                        InternalMessage = $"Order with ID {orderId} was not found."
                    };
                }

                var orderDetails = new OrderDetailsDto
                {
                    OrderId = order.Id,
                    CustomerName = order.Customer.user.Name,
                    Address = $"{order.DeliveryAddress.Street}, {order.DeliveryAddress.City}, {order.DeliveryAddress.Country}",
                    Status = order.Status.ToString(),
                    DeliveryName = order.Delivery?.User?.Name,
                    Items = order.Items.Select(i => new OrderItemDto
                    {
                        ProductName = i.MenuItem.Title,
                        Quantity = i.Quantity,
                        UnitPrice = i.UnitPrice
                    }).ToList(),
                    TotalPrice = order.Items.Sum(i => i.Quantity * i.UnitPrice)
                };

                return new Response<OrderDetailsDto>
                {
                    Data = orderDetails,
                    Status = ResponseStatus.Success,
                    Message = "Order details retrieved successfully",
                    InternalMessage = "Order details retrieved successfully"
                };
            }
            catch (Exception ex)
            {
                return new Response<OrderDetailsDto>
                {
                    Status = ResponseStatus.InternalServerError,
                    Message = "An error occurred while retrieving order details",
                    InternalMessage = ex.Message
                };
            }
        }

        //UpdateOrderStatusAsyncGeneral
        public async Task<Response<bool>> UpdateOrderStatusAsync(int orderId, string status)
        {
            try
            {

                var order = await _unitOfWork.OrderRepository.GetByIdAsync(orderId);
                if (order == null)
                {
                    return new Response<bool>
                    {
                        Status = ResponseStatus.NotFound,
                        Message = "Order not found",
                        InternalMessage = $"Order with ID {orderId} was not found."
                    };
                }
                if (!Enum.TryParse(status, out Enums.OrderStatus orderStatus))
                {
                    return new Response<bool>
                    {
                        Status = ResponseStatus.BadRequest,
                        Message = "Invalid order status",
                        InternalMessage = $"The provided status '{status}' is not valid."
                    };
                }
                order.Status = (Domain.Entities.OrderStatus)orderStatus;
                _unitOfWork.OrderRepository.Update(order);

                return new Response<bool>
                {
                    Data = true,
                    Status = ResponseStatus.Success,
                    Message = "Order status updated successfully",
                    InternalMessage = "Order status updated successfully"
                };
            }
            catch (Exception ex)
            {
                return new Response<bool>
                {
                    Status = ResponseStatus.InternalServerError,
                    Message = "An error occurred while updating the order status",
                    InternalMessage = ex.Message
                };
            }
        }


        //UpdateOrderStatusAsync
        public async Task<Response<DeliveryStatusDto>> UpdateOrderStatusAsync(int orderId, Enums.OrderStatus status)
        {
            try
            {
                var order = await _unitOfWork.OrderRepository.GetByIdAsync(orderId);
                if (order == null)
                {
                    return new Response<DeliveryStatusDto>
                    {
                        Status = ResponseStatus.NotFound,
                        Message = "Order not found",
                        InternalMessage = $"Order with ID {orderId} was not found."
                    };
                }

                order.Status = (Domain.Entities.OrderStatus)status;
                _unitOfWork.OrderRepository.Update(order);
                await _unitOfWork.SaveAsync();

                return new Response<DeliveryStatusDto>
                {
                    Data = new DeliveryStatusDto
                    {
                        id = order.Id, 
                        status = order.Status.ToString() 
                    },
                    Status = ResponseStatus.Success,
                    Message = "Order status updated successfully",
                    InternalMessage = "Order status updated successfully"
                };

               
            }
            catch (Exception ex)
            {
                return new Response<DeliveryStatusDto>
                {
                    Status = ResponseStatus.InternalServerError,
                    Message = "An error occurred while updating the order status",
                    InternalMessage = ex.Message
                };
            }
        }

        public async Task<Response<DeliveryStatusDto>> UpdateOrderStatusFromOntheWaytoDevlived(int orderId)
        {
            return await UpdateOrderStatusAsync(orderId, Enums.OrderStatus.Delivered);

        }

        public async Task<Response<DeliveryStatusDto>> UpdateOrderStatusFromDevlivedtoCancelled(int orderId)
        {
            return await UpdateOrderStatusAsync(orderId, Enums.OrderStatus.Cancelled);
        }
        public async Task<Response<DeliveryStatusDto>> UpdateOrderStatusFromPindingtoOntheWay(int orderId)
        {
            return await UpdateOrderStatusAsync(orderId, Enums.OrderStatus.Shipped);
        }



        public async Task<Response<IEnumerable<OrderStatusFilterDto>>> GetOrdersByStatusAsync(Enums.OrderStatus status)
        {
            try
            {
                var orders = _unitOfWork.OrderRepository.FilterAll(
                    e => e.Status == (Domain.Entities.OrderStatus)status,
                    include: ["Customer.user", "DeliveryAddress", "Delivery.User"], true
                );

                var mappedOrders = orders.Select(o => new OrderForDeliveryDto
                {
                    OrderId = o.Id,
                    CustomerName = o.Customer.user.Name,
                    Address = $"{o.DeliveryAddress.Street}, {o.DeliveryAddress.City}, {o.DeliveryAddress.Country}",
                    Status = o.Status.ToString(),
                    DeliveryName = o.Delivery.User.Name
                });

                return new Response<IEnumerable<OrderStatusFilterDto>>
                {
                    Data = (IEnumerable<OrderStatusFilterDto>)mappedOrders,
                    Status = ResponseStatus.Success,
                    Message = "Orders filtered successfully",
                    InternalMessage = "Orders filtered by status successfully"
                };
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<OrderStatusFilterDto>>
                {
                    Status = ResponseStatus.InternalServerError,
                    Message = "An error occurred while filtering orders",
                    InternalMessage = ex.Message
                };
            }
        }

        public async Task<Response<IEnumerable<OrdersByDeliveryDt>>> GetOrdersByDeliveryAsync(int deliveryId)
        {
            try
            {
                var orders = _unitOfWork.OrderRepository.FilterAll(
                                o => o.DeliveryId == deliveryId,
                                include: ["Customer.user", "DeliveryAddress", "Delivery.User"],
                                true
                            ).ToList();

                if (orders == null || !orders.Any())
                {
                    return new Response<IEnumerable<OrdersByDeliveryDt>>
                    {
                        Status = ResponseStatus.NotFound,
                        Message = "No orders found for this delivery.",
                        InternalMessage = "No orders found for the provided DeliveryId."
                    };
                }

                var mappedOrders = orders.Select(o => new OrdersByDeliveryDt
                {
                    OrderId = o.Id,
                    CustomerName = o.Customer.user.Name,
                    Address = $"{o.DeliveryAddress.Street}, {o.DeliveryAddress.City}, {o.DeliveryAddress.Country}",
                    Status = o.Status.ToString(),
                    DeliveryName = o.Delivery.User.Name
                }).ToList();

                return new Response<IEnumerable<OrdersByDeliveryDt>>
                {
                    Data = mappedOrders,
                    Status = ResponseStatus.Success,
                    Message = "Orders retrieved successfully",
                    InternalMessage = "Orders for delivery retrieved successfully"
                };
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<OrdersByDeliveryDt>>
                {
                    Status = ResponseStatus.InternalServerError,
                    Message = "An error occurred while retrieving orders",
                    InternalMessage = ex.Message
                };
            }
        }

    }
}
