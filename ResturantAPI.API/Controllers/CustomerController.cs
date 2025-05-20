using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ResturantAPI.Domain.Entities;
using ResturantAPI.Domain.Interface;
using ResturantAPI.Services.Dtos;
using ResturantAPI.Services.Enums;
using ResturantAPI.Services.IService;
using ResturantAPI.Services.Model;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ResturantAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IRestaurantService _restaurantService;

        public CustomerController(ICustomerService customerService, IRestaurantService restaurantService)
        {
            _customerService = customerService;
            _restaurantService = restaurantService;
        }

        //api/Customer/CurrentProfile
        [HttpGet("CurrentProfile")]
        public async Task<Response<CustomerProfileDTO>> GetCustomerProfile()
        {
            return await _customerService.GetCustomerProfile();
        }

        //api/Customer/Details/{id}
        [HttpGet("Details/{id}")]
        public async Task<Response<CustomerDTO>> GetCustomer(int id)
        {
            return await _customerService.GetCustomerById(id);
        }

        //api/Customer/UpdateProfile
        [HttpPut("UpdateProfile")]
        public async Task<Response<bool>> UpdateCustomer([FromBody] CustomerUpdateDTO customer)
        {
            if (!ModelState.IsValid)
            {
                return new Response<bool>
                {
                    Data = false,
                    Status = ResponseStatus.NoContent,
                    Message = "Invalid input data."
                };
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return new Response<bool>
                {
                    Data = false,
                    Status = ResponseStatus.Unauthorized,
                    Message = "User is not authenticated."
                };
            }

            return await _customerService.UpdateCustomer(userId, customer);

        }

        //api/Customer/DeleteProfile
        [HttpDelete("DeleteProfile")]
        public async Task<Response<bool>> DeleteCustomer()
        {
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return await _customerService.DeleteCustomer(userId);
        }

        //api/Customer/AddAddresses
        [HttpPost("AddAddresses")]
        public async Task<Response<AddressDTO>> AddAddress([FromBody] AddressDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return new Response<AddressDTO>
                {
                    Data = null,
                    Status = ResponseStatus.NoContent,
                    Message = string.Join("; ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage))
                };
            }
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return new Response<AddressDTO>
                {
                    Data = null,
                    Status = ResponseStatus.Unauthorized,
                    Message = "User not authenticated."
                };
            }
            return await _customerService.AddCustomerAddress(userId, dto);

        }

        //api/Customer/GetAllAddresses
        [HttpGet("GetAllAddresses")]
        public async Task<Response<List<AddressDTO>>> GetAllAddresses()
        {
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return new Response<List<AddressDTO>>
                {
                    Data = null,
                    Status = ResponseStatus.Unauthorized,
                    Message = "User is not authenticated"
                };
            }

             return await _customerService.GetAllAddressesAsync(userId);
           
        }

        //api/Customer/AddOrder
        [HttpPost("AddOrder")]
        public async Task<Response<OrderDTO>> AddOrder([FromBody] OrderDTO orderDto)
        {
            if (!ModelState.IsValid)
            {
                return new Response<OrderDTO>
                {
                    Data = null,
                    Status = ResponseStatus.NoContent,
                    Message = string.Join("; ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage))
                };
            }
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if(string.IsNullOrEmpty(userId))
            {
                return new Response<OrderDTO>
                {
                    Data = null,
                    Status = ResponseStatus.Unauthorized,
                    Message = "User not authenticated."
                };
            }
            return await _customerService.AddOrderAsync(userId, orderDto);

        }

        [HttpGet("GetAllOrders")]
        public async Task<Response<List<OrderDTO>>> GetAllOrders()
        {
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return new Response<List<OrderDTO>>
                {
                    Data = null,
                    Status = ResponseStatus.Unauthorized,
                    Message = "User is not authenticated"
                };
            }
            return await _customerService.GetAllOrdersAsync(userId);
        }

        [HttpPost("AddPayment")]
        public async Task<Response<PaymentDTO>> AddPayment([FromBody] PaymentDTO paymentDTO)
        {
            if (!ModelState.IsValid)
            {
                return new Response<PaymentDTO>
                {
                    Data = null,
                    Status = ResponseStatus.NoContent,
                    Message = string.Join(";", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage))
                };
            }
                string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrWhiteSpace(userId))
                {
                    return new Response<PaymentDTO>
                    {
                        Data = null,
                        Status = ResponseStatus.Unauthorized,
                        Message = "User not authenticated."
                    };
                }

            return await _customerService.AddPaymentAsync(userId, paymentDTO);
            
        }

        [HttpGet("GetAllPayments")]
        public async Task<Response<List<PaymentDTO>>> GetAllPayments()
        {
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return new Response<List<PaymentDTO>>
                {
                    Data = null,
                    Status = ResponseStatus.Unauthorized,
                    Message = "User is not authenticated"
                };
            }
            return await _customerService.GetAllPaymentsAsync(userId);
        }

        [HttpGet("GetAllRestaurants")]
        public async Task<Response<List<RestaurantDTO>>> GetAllRestaurants()
        {
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return new Response<List<RestaurantDTO>>
                {
                    Data = null,
                    Status = ResponseStatus.Unauthorized,
                    Message = "User is not authenticated."
                };
            }

           return await _restaurantService.GetAllRestaurants();
        }
    }
}
