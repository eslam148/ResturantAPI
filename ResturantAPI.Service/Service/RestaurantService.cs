using AutoMapper;
using ResturantAPI.Domain.Entities;
using ResturantAPI.Domain.Interface;
using ResturantAPI.Services.Dtos;
using ResturantAPI.Services.Enums;
using ResturantAPI.Services.IService;
using ResturantAPI.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantAPI.Services.Service
{
    public class RestaurantService:IRestaurantService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RestaurantService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<List<RestaurantDTO>>> GetAllRestaurants()
        {
            try
            {
                IQueryable<Restaurant> restaurants =  _unitOfWork.Restaurant.GetAllAsync(); 
                List<RestaurantDTO> restaurantDtos = _mapper.Map<List<RestaurantDTO>>(restaurants.ToList());
                return new Response<List<RestaurantDTO>>
                {
                    Data = restaurantDtos,
                    Status = ResponseStatus.Success,
                    Message = "Restaurants retrieved successfully."
                };
            }
            catch (Exception ex)
            {
                return new Response<List<RestaurantDTO>>
                {
                    Data = null,
                    Status = ResponseStatus.NotFound,
                    Message = "An error occurred while retrieving restaurants."
                };
            }
        }
    }
}


