using ResturantAPI.Services.Dtos;
using ResturantAPI.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantAPI.Services.IService
{
    public interface IRestaurantService
    {
        Task<Response<List<RestaurantDTO>>> GetAllRestaurants();
    }
}
