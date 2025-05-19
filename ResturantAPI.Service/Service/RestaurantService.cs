using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResturantAPI.Domain.Entities;
using ResturantAPI.Domain.Interface;
using ResturantAPI.Services.Dtos;
using ResturantAPI.Services.Enums;
using ResturantAPI.Services.IService;
using ResturantAPI.Services.Model;
using t7kmplus.Service.Dtos;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ResturantAPI.Services.Service
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RestaurantService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<Response<List<RestaurantDto>>> GetAllRestaurantsAsync()
        {
            try
            {
                List<RestaurantDto> allRestaurant = await _unitOfWork.RestaurantRepository
                        .GetAllAsync()
                        .Select(r => new RestaurantDto { Id = r.Id, Name = r.Name, IsOpen = r.IsOpen, Location = r.Location, ImageUrl = r.ImageUrl })
                        .ToListAsync();

                return new Response<List<RestaurantDto>>()
                {

                    Status = ResponseStatus.Success,
                    Message = $"{allRestaurant.Count} restaurants Successfully fetched",
                    Data = allRestaurant,

                };

            }
            catch (Exception ex)
            {

                return new Response<List<RestaurantDto>>()
                {

                    Status = ResponseStatus.InternalServerError,
                    Message = "Please try again later.",
                    InternalMessage = $"GetAllRestaurantsAsync failed: {ex.Message}",
                    Data = null,
                    SubStatus = 500,

                };

            }
        }
        public async Task<Response<RestaurantDto>> GetRestaurantsByIdAsync(int id)
        {



            try
            {
                var restaurant = await _unitOfWork.RestaurantRepository
                 .GetByIdAsync(id);

                RestaurantDto restaurantDto = new RestaurantDto();
                restaurantDto.Id = restaurant.Id;
                restaurantDto.Name = restaurant.Name;
                restaurantDto.IsOpen = restaurant.IsOpen;
                restaurantDto.ImageUrl = restaurant.ImageUrl;
                restaurantDto.Location = restaurant.Location;


                return new Response<RestaurantDto>
                {
                    Status = ResponseStatus.Success,
                    Message = " restaurants Successfully fetched",
                    Data = restaurantDto,
                };


            }
            catch (Exception ex)
            {

                return new Response<RestaurantDto>()
                {

                    Status = ResponseStatus.InternalServerError,
                    Message = "Please try again later.",
                    InternalMessage = $"GetRestaurantsByIdAsync failed: ",
                    Data = null,
                    SubStatus = 500,

                };

            }




        }
        public async Task<Response<AddRestaurantDto>> AddRestaurantAsync(AddRestaurantDto dto)
        {



            try
            {
                Restaurant restaurant = new Restaurant();


                restaurant.IsOpen = dto.IsOpen;
                restaurant.Name = dto.Name;
                restaurant.UserId = dto.UserId;
                restaurant.ImageUrl = dto.ImageUrl;
                restaurant.Location = dto.Location;

                await _unitOfWork.RestaurantRepository.AddAsync(restaurant);
                return new Response<AddRestaurantDto>
                {

                    Status = ResponseStatus.Success,
                    Message = $"{dto.Name} has been successfully added",
                    Data = dto,
                };

            }
            catch (Exception ex)
            {

                return new Response<AddRestaurantDto>()
                {

                    Status = ResponseStatus.InternalServerError,
                    Message = "An error occurred while adding the restaurant. Please try again later.",
                    InternalMessage = $"AddRestaurantAsync failed: ",
                    Data = null,
                    SubStatus = 500,

                };

            }
        }
        public async Task<Response<Restaurant>> deleteRestaurantAsync(int id)
        {
            Restaurant restaurant = await _unitOfWork.RestaurantRepository.GetByIdAsync(id);
            if (restaurant == null)
            {
                return new Response<Restaurant>()
                {

                    Status = ResponseStatus.NotFound,
                    Message = "Restaurant not found.",
                    InternalMessage = $"DeleteRestaurantAsync failed: ",
                    Data = null,
                    SubStatus = 404,

                };


            }
            try
            {
                _unitOfWork.RestaurantRepository.Delete(restaurant);

                return new Response<Restaurant>()
                {
                    Status = ResponseStatus.Success,
                    Message = $"{restaurant.Name} with id {restaurant.Id} has been successfully deleted",
                    Data = restaurant
                };


            }
            catch (Exception ex)
            {
                return new Response<Restaurant>()
                {

                    Status = ResponseStatus.InternalServerError,
                    Message = "An error occurred while deleting the restaurant. Please try again later.",
                    InternalMessage = $"DeleteRestaurantAsync failed: {ex.Message}",
                    Data = null,
                    SubStatus = 500,

                };

            }

        }
        public async Task<Response<Restaurant>> updateRestaurantAsync(RestaurantDto restaurant, int id)
        {
            Restaurant restaurantFromDB = await _unitOfWork.RestaurantRepository.GetByIdAsync(id);
            if (restaurantFromDB == null)
            {
                return new Response<Restaurant>()
                {

                    Status = ResponseStatus.NotFound,
                    Message = "Restaurant not found.",
                    InternalMessage = $"DeleteRestaurantAsync failed: ",
                    Data = null,
                    SubStatus = 404,

                };

            }
            try
            {
                restaurantFromDB.Name = restaurant.Name;
                restaurantFromDB.IsOpen = restaurant.IsOpen;
                restaurantFromDB.Location = restaurant.Location;
                restaurantFromDB.ImageUrl = restaurant.ImageUrl;

                _unitOfWork.RestaurantRepository.Update(restaurantFromDB);
                return new Response<Restaurant>()
                {
                    Status = ResponseStatus.Success,
                    Message = $"{restaurant.Name} with id {restaurant.Id} has been successfully updateded",

                };


            }
            catch (Exception ex)
            {
                return new Response<Restaurant>()
                {

                    Status = ResponseStatus.InternalServerError,
                    Message = "An error occurred while updating the restaurant. Please try again later.",
                    InternalMessage = $"DeleteRestaurantAsync failed: {ex.Message}",
                    Data = null,
                    SubStatus = 500,

                };

            }
        }
    }
}




    




        

    



