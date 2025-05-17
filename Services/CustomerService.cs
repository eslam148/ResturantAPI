using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ResturantAPI.Domain.Entities;
using ResturantAPI.Services.Dtos;
using System.Threading.Tasks;

namespace ResturantAPI.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CustomerService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Customer> UpdateCustomerAsync(int id, CustomerUpdateDTO updateDto)
        {
            var customer = await _context.Customers
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (customer == null)
                return null;

            // Update the associated ApplicationUser properties
            customer.User.Name = updateDto.Name;
            customer.User.Email = updateDto.Email;
            customer.User.PhoneNumber = updateDto.PhoneNumber;

            await _context.SaveChangesAsync();
            return customer;
        }
    }

    public interface ICustomerService
    {
        Task<Customer> UpdateCustomerAsync(int id, CustomerUpdateDTO updateDto);
    }
} 