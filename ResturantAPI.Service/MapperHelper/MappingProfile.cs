using AutoMapper;
using ResturantAPI.Domain.Entities;
using ResturantAPI.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantAPI.Services.MapperHelper
{
   public  class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerProfileDTO>()
            .ForMember(des => des.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User != null ? src.User.Name : string.Empty))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User != null ? src.User.Email : string.Empty))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User != null ? src.User.PhoneNumber : string.Empty));

            CreateMap<Customer, CustomerDTO>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.User.Name))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
            .ReverseMap();

           
            CreateMap<Payment, PaymentDTO>()
                .ForMember(dest => dest.CreditCardNumber, opt => opt.MapFrom(src => "**** **** **** " + src.CreditCardNumber.Substring(src.CreditCardNumber.Length - 4)))
                .ReverseMap();

            CreateMap<Order, OrderDTO>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.PaymentMethod, opt => opt.MapFrom(src => src.paymentMethod.ToString()))
                .ReverseMap();

            CreateMap<CustomerUpdateDTO, Customer>()
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => new ApplicationUser
            {
                Name = src.Name,
                Email = src.Email,
                PhoneNumber = src.PhoneNumber
            }));

            CreateMap<Address, AddressDTO>().ReverseMap();

            CreateMap<Restaurant, RestaurantDTO>().ReverseMap();

        }
    }
}
