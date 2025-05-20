using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ResturantAPI.Domain;
using ResturantAPI.Domain.Entities;
using ResturantAPI.Domain.Interface;
using ResturantAPI.Services.Dtos.ReportDTO;
using ResturantAPI.Services.Dtos.ResturntReportDTO;
using ResturantAPI.Services.Enums;
using ResturantAPI.Services.IService;
using ResturantAPI.Services.Model;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ResturantAPI.Services.Service
{
    public class ReportServices : IReportServices
    {
        private readonly IUnitOfWork unitOfWork;

        public ReportServices(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        //public Response<AllResturantDto> 
        public Response<IEnumerable<AllResturantDto>> GetAllResturantAsync(bool track = false)
        {
            IQueryable<AllResturantDto> resturants =  unitOfWork.Restaurant
                .GetAllAsync(track)
                .Select(r => new AllResturantDto
                {
                    Id = r.Id,
                    Name = r.Name,
                    Location = r.Location,
                    OrderCounter = r.Orders.Count,
                });

            return new Response<IEnumerable<AllResturantDto>>
            {
                Data = resturants,
                Status = ResponseStatus.Success,
                Message = "There is all resturants"
            };
        }

        public async Task<Response<int>> GetRestaurantCounterAsync()
        {
            int ressturantCounters = await unitOfWork.Restaurant
                .GetAllAsync()
                .CountAsync();

            return new Response<int>
            {
                Data = ressturantCounters,
                Status = ResponseStatus.Success,
                Message = "there is count of resturant "
            };
        }

        public async Task<Response<PagedResult<AllResturantDto>>> GetPaginatedForRestaurantAsync(int pageNumber, int pageSize, Expression<Func<Restaurant, object>>? orderExpression = null)
        {
            PagedResult<Restaurant> restaurant = await unitOfWork.Restaurant
                .GetPaginatedAsync(1, 10, r => r.Name);

            IQueryable<AllResturantDto> resturantDtos = restaurant.Items
                .Select(r => new AllResturantDto
                {
                    Name = r.Name,
                    Id = r.Id,
                    Location = r.Location,
                    OrderCounter = r.Orders.Count
                });

            return new Response<PagedResult<AllResturantDto>>
            {
                Data = new PagedResult<AllResturantDto>
                {
                    Items = resturantDtos,
                    PageNumber = restaurant.PageNumber,
                    PageSize = restaurant.PageSize,
                    TotalCount = restaurant.TotalCount,
                },
                Status = ResponseStatus.Success,
                Message = "here pagenate data"
            };
        }

        public Response<IEnumerable<AllResturantDto>> FilterAllRestaurant(Expression<Func<Restaurant, bool>> predicate = default, bool track = false)
        {
            IQueryable<Restaurant> allResturants;
            if(predicate == null)
            {
                allResturants = unitOfWork.Restaurant
                    .GetAllAsync();
            }
            else
            {
                allResturants = unitOfWork.Restaurant
                    .FilterAll(predicate);
            }

            return new Response<IEnumerable<AllResturantDto>>
            {
                Data = allResturants.Select(r => new AllResturantDto
                {
                    Name = r.Name,
                    Id = r.Id,
                    Location = r.Location,
                    OrderCounter = r.Orders.Count
                }),
                Message = "there is all resturants",
                Status = ResponseStatus.Success,
            };

        }

        public async Task<Response<decimal>> AverageOrdersPriceAsync(int restaurantId, DateOnly dateOnly)
        {
            IQueryable<Order> orders;
            if (dateOnly != default)
            {
                DateTime startTime = dateOnly.ToDateTime(TimeOnly.MinValue);
                DateTime endTime = dateOnly.ToDateTime(TimeOnly.MaxValue);

                orders = unitOfWork.Order.GetAllAsync()
                    .Where(o => o.RestaurantId == restaurantId && o.OrderDate >= startTime && o.OrderDate <= endTime);
            }
            else
            {
                orders = unitOfWork.Order.GetAllAsync()
                    .Where(o => o.RestaurantId == restaurantId);
            }

            decimal avrageOrdersPrice = await orders.AverageAsync(o => o.TotalAmount);

            return new Response<decimal>
            {
                Data = avrageOrdersPrice,
                Status = ResponseStatus.Success,
                Message = "there is average prices of orders"
            };
        }

        /************---------*********---------******************-------------**********----------**************/

        public Response<IEnumerable<AllDeliveryOrder>> GetAllDelivery(bool track = false)
        {
            IQueryable < AllDeliveryOrder> deliveries  = unitOfWork.Delivery
                .GetAllAsync()
                .Include(d => d.User)
                .Select(d => new AllDeliveryOrder
                {
                    Id = d.Id,
                    Name = d.User.Name,
                    OrderCounter = d.Orders.Count,
                });
            return new Response<IEnumerable<AllDeliveryOrder>>
            {
                Data = deliveries,
                Message = "there is deliverys",
                Status = ResponseStatus.Success
            };
        }

        public Response<IEnumerable<AllDeliveryOrder>> FilterAllDelivery(Expression<Func<Delivery, bool>> predicate = default, bool track = false)
        {
            IQueryable<AllDeliveryOrder> deliveries = unitOfWork.Delivery
                .FilterAll(predicate, track)
                .Include(d => d.User)
                .Select(d => new AllDeliveryOrder
                {   
                    Id = d.Id,
                    Name = d.User.Name,
                    OrderCounter = d.Orders.Count
                });

            return new Response<IEnumerable<AllDeliveryOrder>>
            {
                Data = deliveries,
                Status = ResponseStatus.Success,
                Message = "there is deliverys"
            };
        }

        public async Task<Response<PagedResult<AllDeliveryOrder>>> GetPaginatedForDeliveryAsync(int pageNumber, int pageSize, Expression<Func<Delivery, object>>? orderExpression = null)
        {
            PagedResult<Delivery> delivery = await unitOfWork.Delivery
                    .GetPaginatedAsync(pageNumber, pageSize, orderExpression);

            IQueryable<AllDeliveryOrder> deliveriesDto = delivery.Items
                    .Select(d => new AllDeliveryOrder
                    {
                        Id = d.Id,
                        Name = d.User.Name,
                        OrderCounter = d.Orders.Count
                    });

            return new Response<PagedResult<AllDeliveryOrder>>
            {
                Data = new PagedResult<AllDeliveryOrder>
                {
                    Items = deliveriesDto,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalCount = delivery.TotalCount
                },
                Status = ResponseStatus.Success,
                Message = "There is data after pagenated"
            };
        }

        public async Task<Response<int>> GetDeliveryCounterAsync()
        {
            int deliveryCount = await unitOfWork.Delivery
                .GetAllAsync()
                .CountAsync();

            return new Response<int>
            {
                Data = deliveryCount,
                Message = "there is counter of delivery",
                Status = ResponseStatus.Success,
            };
        }

        /*public async Task<int> GetDeliveryOrdersCount(int deliveryID)
        {
            Delivery delivery = await unitOfWork.Delivery
                .GetAllAsync()
                .Include(d => d.Orders)
                .Where(d => d.Id == deliveryID)
                .FirstOrDefaultAsync();

            return delivery.Orders.Count;
        }*/

        public async Task <Response<int>> GetDeliveryOrdersCountAsync(int deliveryID, DateOnly dateOnly = default   )
        {
            IQueryable<Order> orders;
            if (dateOnly != default)
            {
                DateTime startTime = dateOnly.ToDateTime(TimeOnly.MinValue);
                DateTime endTime = dateOnly.ToDateTime(TimeOnly.MaxValue);

                orders = unitOfWork.Order.GetAllAsync()
                    .Where(o => o.DeliveryId == deliveryID && o.OrderDate >= startTime && o.OrderDate <= endTime);
            }
            else
            {
                orders = unitOfWork.Order.GetAllAsync()
                    .Where(o => o.DeliveryId == deliveryID);
            }

            return new Response<int>
            {
                Data = await orders.CountAsync(),
                Message = "there is count of delivery Order",
                Status = ResponseStatus.Success,
            };    
        }

/******************------------------********---------------------*********************/
            /**********--------------------------Customer----------------------------**************/
        public Response<IEnumerable<AllCustomerDto>> GetAllCustomerInResturant(string address = default, DateOnly dateOnly = default)
        {   
            IQueryable<Customer> customer = unitOfWork.Customer
                .GetAllAsync()
                .Include(c => c.Addresses)
                .Include(c => c.user)
                .Include(c => c.Orders);

            if(dateOnly != default)
            {
                DateTime startTime = dateOnly.ToDateTime(TimeOnly.MinValue);
                DateTime endTime = dateOnly.ToDateTime(TimeOnly.MaxValue);

                customer = customer.Where(c => c.Orders.Any(o => o.OrderDate >= startTime && o.OrderDate <= endTime));
            }
            if (!string.IsNullOrEmpty(address))
            {
                customer = customer.Where(c => c.Addresses.Any(a => a.City.Contains(address)));
            }

            IQueryable<AllCustomerDto> customers = customer.Select(c => new AllCustomerDto
            {
                Id = c.Id,
                Name = c.user.Name,
                City = c.Addresses.Select(c => c.City).FirstOrDefault(),
                OrderCounter = c.Orders.Count
            });

            return new Response<IEnumerable<AllCustomerDto>>
            {
                Data = customers,
                Status = ResponseStatus.Created,
                Message = "here data of customer",
            };
        }

        public async Task<Response<CustomerDto>> CustomerOrderCounter(int customerId, DateOnly dateOnly = default)
        {
            IQueryable<Customer> customers = unitOfWork.Customer.GetAllAsync();
            if (dateOnly != default)
            {
                DateTime startTime = dateOnly.ToDateTime(TimeOnly.MinValue);
                DateTime endTime = dateOnly.ToDateTime(TimeOnly.MaxValue);

                customers = customers.Where(c => c.Id == customerId && (c.CreatedAt >= startTime && c.CreatedAt <= endTime));
            }
            else
            {
                customers.Where(c => c.Id == customerId);
            }

            CustomerDto customerDto = await customers.Select(c => new CustomerDto
            {
                Id = c.Id,
                Name = c.user.Name,
                OrderCounter = c.Orders.Count
            }).FirstOrDefaultAsync();

            if (customerDto.OrderCounter >= 1)
            {
                return new Response<CustomerDto>
                {
                    Data = customerDto,
                    Status = ResponseStatus.Success,
                    Message = "There is counter of customer order",
                };
            }
            else
            {
                return new Response<CustomerDto>
                {
                    Data = customerDto,
                    Status = ResponseStatus.Success,
                    Message = "There customer doesn't have any order",
                };
            }
        }




    }
}
