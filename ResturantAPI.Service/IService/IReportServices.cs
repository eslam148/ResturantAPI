using ResturantAPI.Domain;
using ResturantAPI.Domain.Entities;
using ResturantAPI.Domain.Interface;
using ResturantAPI.Services.Dtos.ResturntReportDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ResturantAPI.Services.Model;

using System.Text;
using System.Threading.Tasks;
using ResturantAPI.Services.Dtos.ReportDTO;

namespace ResturantAPI.Services.IService
{
    public interface IReportServices
    {
        Response<IEnumerable<AllResturantDto>> GetAllResturantAsync(bool track = false);
        //Response<IEnumerable<AllResturantDto>> FilterAllRestaurant(Expression<Func<Restaurant, bool>> predicate = default, bool track = false);
        Task<Response<PagedResult<AllResturantDto>>> GetPaginatedForRestaurantAsync(int pageNumber, int pageSize, Expression<Func<Restaurant, object>>? orderExpression = null);
        Task<Response<int>> GetRestaurantCounterAsync();
        Task<Response<decimal>> AverageOrdersPriceAsync(int restaurantId, DateOnly dateOnly);
        /***********------------- Delivery -------------------*/
        Response<IEnumerable<AllDeliveryOrder>> GetAllDelivery(bool track = false);
        Response<IEnumerable<AllDeliveryOrder>> FilterAllDelivery(Expression<Func<Delivery, bool>> predicate = default, bool track = false);
        Task<Response<PagedResult<AllDeliveryOrder>>> GetPaginatedForDeliveryAsync(int pageNumber, int pageSize, Expression<Func<Delivery, object>>? orderExpression = null);
        Task<Response<int>> GetDeliveryCounterAsync();
        Task<Response<int>> GetDeliveryOrdersCountAsync(int deliveryID, DateOnly dateOnly = default);
        /**********--------------------------Customer----------------------------**************/
        Response<IEnumerable<AllCustomerDto>> GetAllCustomerInResturant(string address = default, DateOnly dateOnly = default);
        Task<Response<CustomerDto>> CustomerOrderCounter(int customerId, DateOnly dateOnly = default);


    }
}
