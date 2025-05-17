using Microsoft.AspNetCore.Mvc;
using ResturantAPI.Services;
using ResturantAPI.Services.Dtos;
using System.Threading.Tasks;

namespace ResturantAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] CustomerUpdateDTO updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedCustomer = await _customerService.UpdateCustomerAsync(id, updateDto);

            if (updatedCustomer == null)
                return NotFound();

            return Ok(updatedCustomer);
        }
    }
} 