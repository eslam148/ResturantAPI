using ResturantAPI.Domain.Entities;
using ResturantAPI.Services.Dtos;
using ResturantAPI.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantAPI.Services.IService
{
    public interface ICustomerService
    {
        Task<Response<CustomerProfileDTO>> GetCustomerProfile();

        Task<Response<CustomerDTO>> GetCustomerById(int id);

        Task<Response<bool>> UpdateCustomer(string userId, CustomerUpdateDTO customerDto);

        Task<Response<bool>> DeleteCustomer(string? userId);

        Task<Response<AddressDTO>> AddCustomerAddress(string userId, AddressDTO addressDto);
        Task<Response<List<AddressDTO>>> GetAllAddressesAsync(string userId);

        Task<Response<List<OrderDTO>>> GetAllOrdersAsync(string userId);
        Task<Response<OrderDTO>> AddOrderAsync(string userId, OrderDTO orderDTO);

        Task<Response<PaymentDTO>> AddPaymentAsync(string userId, PaymentDTO paymentDTO);
        Task<Response<List<PaymentDTO>>> GetAllPaymentsAsync(string userId);

    }
}
