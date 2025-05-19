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
        private readonly IGeneralRepository<Restaurant, int> generalRepository;

        ReportServices(IUnitOfWork unitOfWork, IGeneralRepository<Restaurant, int> generalRepository)
        {
            this.unitOfWork = unitOfWork;
            this.generalRepository = generalRepository;
        }
        //public Response<AllResturantDto> 
        public Response<IEnumerable<AllResturantDto>> GetAllResturantAsync(bool track = false)
        {
            IQueryable<AllResturantDto> resturants =  unitOfWork.RestaurantRepository
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

        public async Task<Response<int>> GetRestaurantCounter()
        {
            int ressturantCounters = await unitOfWork.RestaurantRepository
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
            PagedResult<Restaurant> restaurant = await unitOfWork.RestaurantRepository
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
            IQueryable<AllResturantDto> allResturants = unitOfWork.RestaurantRepository
                .FilterAll(predicate)
                .Select(r => new AllResturantDto
                {
                    Name = r.Name,
                    Id = r.Id,
                    Location = r.Location,
                    OrderCounter = r.Orders.Count
                });

            return new Response<IEnumerable<AllResturantDto>>
            {
                Data = allResturants,
                Message = "there is all resturants",
                Status = ResponseStatus.Success,
            };

        }

        /*public async Task<decimal> AveragePriceItem(int restaurantId, DateOnly dateOnly)
        {
            DateTime startTime = dateOnly.ToDateTime(TimeOnly.MinValue);
            DateTime endTime = dateOnly.ToDateTime(TimeOnly.MaxValue);
            var items = await unitOfWork.Menu.GetAllAsync()
                .Include(m => m.Items)
                .Where(m => m.RestaurantId == restaurantId && m..CreatedAt >= startTime && m.CreatedAt <= endTime)
                .SelectMany(m => m.Items)
                .ToListAsync();

            var averagePrice = items.Average(i => i.Price);
            return averagePrice;
        }*/
        public async Task<Response<decimal>> AverageOrdersPrice(int restaurantId, DateOnly dateOnly)
        {
            IQueryable<Order> orders;
            if (dateOnly != null)
            {
                DateTime startTime = dateOnly.ToDateTime(TimeOnly.MinValue);
                DateTime endTime = dateOnly.ToDateTime(TimeOnly.MaxValue);

                orders = unitOfWork.OrderRepository.GetAllAsync()
                    .Where(o => o.RestaurantId == restaurantId && o.OrderDate >= startTime && o.OrderDate <= endTime);
            }
            else
            {
                orders = unitOfWork.OrderRepository.GetAllAsync()
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
            IQueryable < AllDeliveryOrder> deliveries  = unitOfWork.DeliveryRepository
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
            IQueryable<AllDeliveryOrder> deliveries = unitOfWork.DeliveryRepository
                .FilterAll(predicate, track)
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
            PagedResult<Delivery> delivery = await unitOfWork.DeliveryRepository
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
            int deliveryCount = await unitOfWork.DeliveryRepository
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
            if (dateOnly != null)
            {
                DateTime startTime = dateOnly.ToDateTime(TimeOnly.MinValue);
                DateTime endTime = dateOnly.ToDateTime(TimeOnly.MaxValue);

                orders = unitOfWork.OrderRepository.GetAllAsync()
                    .Where(o => o.DeliveryId == deliveryID && o.OrderDate >= startTime && o.OrderDate <= endTime);
            }
            else
            {
                orders = unitOfWork.OrderRepository.GetAllAsync()
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
            /*IQueryable<AllCustomerDto> customers;
            if (address == null && dateOnly == null)
            {
                customers = unitOfWork.Customer
                .GetAllAsync()
                .Include(c => c.Addresses)
                .Include(c => c.user)
                .Include(c => c.Orders)
                .Select(c => new AllCustomerDto
                {
                    Id = c.Id,
                    Name = c.user.Name,
                    City = c.Addresses.Select(c => c.City).FirstOrDefault(),
                    OrderCounter = c.Orders.Count
                });
            }
            else
            {
                DateTime startTime = dateOnly.ToDateTime(TimeOnly.MinValue);
                DateTime endTime = dateOnly.ToDateTime(TimeOnly.MaxValue);

                customers = unitOfWork.Customer
                    .GetAllAsync()
                    .Include(c => c.Addresses)
                    .Include(c => c.user)
                    .Where(c => c.Addresses.Any(a => a.City == address)
                        && c.Orders.Any(o => o.OrderDate >= startTime && o.OrderDate <= endTime))
                    .Select(c => new AllCustomerDto
                    {
                        Id = c.Id,
                        Name = c.user.Name,
                        City = c.Addresses.Select(c => c.City).FirstOrDefault(),
                        OrderCounter = c.Orders.Count
                    });
            }
            return new Response<IEnumerable<AllCustomerDto>>
            {
                Data = customers,
                Status = ResponseStatus.Created,
                Message = "here data of customer"
            };*/
            
            IQueryable<Customer> customer = unitOfWork.CustomerRepository
                .GetAllAsync()
                .Include(c => c.Addresses)
                .Include(c => c.User)
                .Include(c => c.Orders);

            if(!(dateOnly == null))
            {
                DateTime startTime = dateOnly.ToDateTime(TimeOnly.MinValue);
                DateTime endTime = dateOnly.ToDateTime(TimeOnly.MaxValue);

                customer = customer.Where(c => c.Orders.Any(o => o.OrderDate >= startTime && o.OrderDate <= endTime));
            }
            if (!string.IsNullOrEmpty(address))
            {
                customer = customer.Where(c => c.Addresses.Any(a => a.City == address));
            }

            IQueryable<AllCustomerDto> customers = customer.Select(c => new AllCustomerDto
            {
                Id = c.Id,
                Name = c.User.Name,
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



    }
}
