using AutoMapper;
using Microsoft.Extensions.Logging;
using ResturantAPI.Domain.Entities;
using ResturantAPI.Domain.Interface;
using ResturantAPI.Services.Dtos;
using ResturantAPI.Services.Enums;
using ResturantAPI.Services.IService;
using ResturantAPI.Services.Model;

namespace RestaurantAPI.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerRepository _customerRepository;
        private readonly IAuthServices _authServices;
        private readonly IMapper _mapper;
   

        public CustomerService(
            IUnitOfWork unitOfWork,
            ICustomerRepository customerRepository,
            IAuthServices authServices,
            IMapper mapper
         
            )
        {
            _unitOfWork = unitOfWork;
            _customerRepository = customerRepository;
            _authServices = authServices;
            _mapper = mapper;
        
        }

        public async Task<Response<AddressDTO>> AddCustomerAddress(string userId, AddressDTO addressDto)
        {
            try
            {
                Customer? customer = await _customerRepository.GetByUserIdAsync(userId, include: [ "Addresses", "User"], track: true);
                if (customer == null)
                    return new Response<AddressDTO>
                    {
                        Data = null,
                        Status = ResponseStatus.NotFound,
                        Message = "Customer not found."
                    };

                Address address = _mapper.Map<Address>(addressDto);
                address.UserId = customer.UserId;
                customer.Addresses.Add(address);

                _unitOfWork.CustomerRepository.Update(customer);
                await _unitOfWork.SaveAsync();

                AddressDTO created = _mapper.Map<AddressDTO>(address);
                return new Response<AddressDTO>
                {
                    Data = created,
                    Status = ResponseStatus.Success,
                    Message = "Address added successfully."
                };
            }
            catch(Exception ex)
            {
                return new Response<AddressDTO>
                {
                    Data = null,
                    Status = ResponseStatus.NotFound,
                    Message = "An error occurred while adding address."
                };
            }
        }

        public async Task<Response<bool>> DeleteCustomer(string? userId)
        {
            try
            {
                if(string.IsNullOrWhiteSpace(userId))
                {
                    return  new Response<bool>
                    {
                        Data = false,
                        Status = ResponseStatus.Unauthorized,
                        Message = "User is not authenticated."
                    };
                }
               
                Customer? customer =await _customerRepository.GetByUserIdAsync(userId, null, true);

                if (customer == null)
                    return new Response<bool>
                    {
                        Data = false,
                        Status = ResponseStatus.NotFound,
                        Message = $"Customer with user-ID {userId} not found."
                    };
                _unitOfWork.CustomerRepository.Delete(customer);
                await _unitOfWork.SaveAsync();

                return new Response<bool>
                {
                    Data = true,
                    Status = ResponseStatus.Success,
                    Message = "Customer deleted successfully."
                };
            }
            catch (Exception ex)
            {
                return new Response<bool>
                {
                    Data = false,
                    Status = ResponseStatus.NotFound,
                    Message = "An error occurred while deleting the customer."
                };
            }
        }

        public async Task<Response<List<AddressDTO>>> GetAllAddressesAsync(string userId)
        {
            Customer? customer = await _unitOfWork.CustomerRepository.GetByUserIdAsync(userId, ["Addresses"]);
            if(customer==null)
            {
                return new Response<List<AddressDTO>>
                {
                    Data = null,
                    Status = ResponseStatus.NotFound,
                    Message = "Customer not found"
                };
            }
            List<AddressDTO> dtoList = _mapper.Map<List<AddressDTO>>(customer.Addresses);

            return new Response<List<AddressDTO>>
            {
                Data = dtoList,
                Status = ResponseStatus.Success,
                Message = "Addresses retrieved successfully"
            };

        }

        public async Task<Response<CustomerDTO>> GetCustomerById(int id)
        {
            try
            {
                Customer? customer = await _customerRepository.GetByIdAsync(id, ["User", "Addresses"]);
                if (customer == null)
                {
                    return new Response<CustomerDTO>
                    {
                        Data = null,
                        Status = ResponseStatus.NotFound,
                        Message = $"Customer with ID {id} not found."
                    };
                }

                return new Response<CustomerDTO>
                {
                    Data = _mapper.Map<CustomerDTO>(customer),
                    Status = ResponseStatus.Success,
                    Message = "Customer retrieved successfully."
                };
            }
            catch (Exception)
            {

                return new Response<CustomerDTO>
                {
                    Data = null,
                    Status = ResponseStatus.NotFound,
                    Message = "An error occurred while retrieving the customer."
                };
            }

        }

        public async Task<Response<CustomerProfileDTO>> GetCustomerProfile()
        {
            string userId ; 
            try
            {

                userId = await _authServices.GetCurrentUserId();
                if (string.IsNullOrWhiteSpace(userId))
                {
                    return new Response<CustomerProfileDTO>
                    {
                        Data = null,
                        Status = ResponseStatus.Unauthorized,
                        Message = "User is not authenticated."
                    };
                }

                Customer? customer = await _customerRepository.GetByUserIdAsync(userId, ["User", "Orders", "Addresses", "Payments"]);
                if (customer == null)
                {
                    return new Response<CustomerProfileDTO>
                    {
                        Data = null,
                        Status = ResponseStatus.NotFound,
                        Message = $"Customer with ID {userId} not found."
                    };
                }

                return new Response<CustomerProfileDTO>
                {
                    Data = _mapper.Map<CustomerProfileDTO>(customer),
                    Status = ResponseStatus.Success,
                    Message = "Customer profile retrieved successfully."
                };
            }
            catch (Exception ex)
            {
                return new Response<CustomerProfileDTO>
                {
                    Data = null,
                    Status = ResponseStatus.NotFound,
                    Message = "An error occurred while retrieving the customer profile."
                };
            }
        }

        public async Task<Response<bool>> UpdateCustomer(string userId, CustomerUpdateDTO customerDto)
        {
            try
            {
                if(string.IsNullOrWhiteSpace(userId))
                {
                    return new Response<bool>
                    {
                        Data = false,
                        Status = ResponseStatus.Unauthorized,
                        Message = "User is not authenticated."
                    };
                }
                Customer? customer = await _customerRepository.GetByUserIdAsync(userId, [ "User" ], true);
                if (customer == null)
                {
                    return new Response<bool>
                    {
                        Data = false,
                        Status = ResponseStatus.NotFound,
                        Message = $"Customer with ID {userId} not found."
                    };
                }

                customer.User.Name = customerDto.Name;
                customer.User.Email = customerDto.Email;
                customer.User.PhoneNumber = customerDto.PhoneNumber;

                _unitOfWork.CustomerRepository.Update(customer);
                await _unitOfWork.SaveAsync();

                return new Response<bool>
                {
                    Data = true,
                    Status = ResponseStatus.Success,
                    Message = "Customer updated successfully."
                };
            }
            catch(Exception ex)
            {
                return new Response<bool>
                {
                    Data = false,
                    Status = ResponseStatus.NotFound,
                    Message = "An error occurred while updating the customer."
                };

            }
        }
    }
}