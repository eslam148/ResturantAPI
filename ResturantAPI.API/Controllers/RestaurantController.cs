using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResturantAPI.Domain.Entities;
using ResturantAPI.Services.Dtos;
using ResturantAPI.Services.Enums;
using ResturantAPI.Services.IService;
using ResturantAPI.Services.Model;

namespace ResturantAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;
        public RestaurantController(IRestaurantService restaurantService)
        {

            _restaurantService = restaurantService;

        }

        [HttpGet]
        public async Task<Response<List<RestaurantDto>>> GetAllRestaurants()
        {
         
                return await _restaurantService.GetAllRestaurantsAsync();

        }
        [HttpGet("{id:int}")]
        public async Task<Response<RestaurantDto>> GetARestaurantById(int id)
        {
            return await _restaurantService.GetRestaurantsByIdAsync(id);
        }
        //public async Task AddARestaurant(AddRestaurantDto restaurantDto)
        //{
        //     await _restaurantService.AddRestaurantAsync(restaurantDto);
        //}
    }
}