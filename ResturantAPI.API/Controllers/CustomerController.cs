using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        //api/Customer/Profile
        [HttpGet("Profile")]
        public async Task<Response<CustomerProfileDTO>> GetCustomerProfile()
        {
            return await _customerService.GetCustomerProfile();
        }

        //api/Customer/5
        [HttpGet("{id}")]
        public async Task<Response<CustomerDTO>> GetCustomer(int id)
        {
            return await _customerService.GetCustomerById(id);
        }

        //api/customer
        [HttpPut]
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

        //api/customer
        [HttpDelete]
        public async Task<Response<bool>> DeleteCustomer()
        {
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return await _customerService.DeleteCustomer(userId);
        }


        [HttpPost("address")]
        public async Task<Response<AddressDTO>> AddAddress([FromBody] AddressDTO dto)
        {
            if (!ModelState.IsValid)
                return new Response<AddressDTO>
                {
                    Data = null,
                    Status = ResponseStatus.NoContent,
                    Message = "Invalid address data."
                };

            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return new Response<AddressDTO>
                {
                    Data = null,
                    Status = ResponseStatus.Unauthorized,
                    Message = "User not authenticated."
                };

            return await _customerService.AddCustomerAddress(userId, dto);

        }

        [HttpGet("addresses")]
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
    }
}
