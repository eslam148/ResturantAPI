using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResturantAPI.Domain;
using ResturantAPI.Domain.Entities;
using ResturantAPI.Services.Dtos.ReportDTO;
using ResturantAPI.Services.Dtos.ResturntReportDTO;
using ResturantAPI.Services.IService;
using ResturantAPI.Services.Model;
using System.Linq.Expressions;

namespace ResturantAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize("Admin")]
    public class ReportController : ControllerBase
    {
        private readonly IReportServices services;

        public ReportController(IReportServices services)
        {
            this.services = services;
        }

        [HttpGet]
        [Route("AllResturant")]

        public Response<IEnumerable<AllResturantDto>> GetAllResturantAsync()
        {
            return services.GetAllResturantAsync();
        }

        [HttpGet]
        [Route("GetRestaurantCounter")]
        public async Task<Response<int>> GetRestaurantCounterAsync()
        {
            return await services.GetRestaurantCounterAsync();
        }

        /*[HttpGet]
        [Route("FilterAllRestaurant")]
        public Response<IEnumerable<AllResturantDto>> FilterAllRestaurant(string name = default , string address = default)
        {
            return services.FilterAllRestaurant(r => r.Name == name || r.Location == address);
        }*/

        [HttpGet]
        [Route("GetPaginatedForRestaurant")]
        public Task<Response<PagedResult<AllResturantDto>>> GetPaginatedForRestaurantAsync(int pageNumber = 1, int pageSize = 10)
        {
            return services.GetPaginatedForRestaurantAsync(pageNumber, pageSize);
        }

        [HttpGet]
        [Route("AverageOrdersPrice/{restaurantId:int}")]
        public async Task<Response<decimal>> AverageOrdersPriceAsync(int restaurantId, DateOnly dateOnly)
        {
            return await services.AverageOrdersPriceAsync(restaurantId, dateOnly);
        }

        /**************-------------------Delivary---------------************/



        [HttpGet]
        [Route("GetAllDelivery")]
        public Response<IEnumerable<AllDeliveryOrder>> GetAllDelivery()
        {
            return services.GetAllDelivery();
        }
        
        [HttpGet]
        [Route("FilterAllDelivery")]
        public Response<IEnumerable<AllDeliveryOrder>> FilterAllDelivery(string name)
        {
            return services.FilterAllDelivery(d => d.User.Name == name);
        }

        [HttpGet]
        [Route("GetPaginatedForDelivery")]
        public async Task<Response<PagedResult<AllDeliveryOrder>>> GetPaginatedForDeliveryAsync(int pageNumber = 1, int pageSize = 10)
        {
            return await services.GetPaginatedForDeliveryAsync(pageNumber, pageSize);
        }

        [HttpGet]
        [Route("GetDeliveryCounter")]
        public async Task<Response<int>> GetDeliveryCounterAsync()
        {
            return await services.GetDeliveryCounterAsync();
        }

        [HttpGet]
        [Route("GetDeliveryOrdersCount/{deliveryID:int}")]
        public async Task<Response<int>> GetDeliveryOrdersCountAsync(int deliveryID, DateOnly dateOnly)
        {
            return await services.GetDeliveryOrdersCountAsync(deliveryID, dateOnly);
        }

        /***********------------------------Customer-----------------------****************/

        [HttpGet]
        [Route("GetAllCustomerInResturant")]
        public Response<IEnumerable<AllCustomerDto>> GetAllCustomerInResturant(string address, DateOnly date)
        {
            return services.GetAllCustomerInResturant(address, date);
        }

        [HttpGet]
        [Route("CustomerOrderCounter/{customerId:int}")]
        public async Task<Response<CustomerDto>> CustomerOrderCounter(int customerId, DateOnly dateOnly = default)
        {
            return await services.CustomerOrderCounter(customerId, dateOnly);
        }


    }
}
