using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResturantAPI.Domain.Entities;
using ResturantAPI.Services.Dtos;
using ResturantAPI.Services.Model;

namespace ResturantAPI.Services.IService
{
   public interface IRestaurantService
    {
       Task<Response<List<RestaurantDto>>> GetAllRestaurantsAsync();
        Task<Response<RestaurantDto>> GetRestaurantsByIdAsync(int id);
        Task<Response<AddRestaurantDto>> AddRestaurantAsync(AddRestaurantDto dto);
        Task<Response<Restaurant>> deleteRestaurantAsync(int id);
        Task<Response<Restaurant>> updateRestaurantAsync(RestaurantDto restaurant, int id);

    }
}
