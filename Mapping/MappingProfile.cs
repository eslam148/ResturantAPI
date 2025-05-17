using AutoMapper;
using ResturantAPI.Domain.Entities;
using ResturantAPI.Services.Dtos;

namespace ResturantAPI.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CustomerUpdateDTO, Customer>()
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.Orders, opt => opt.Ignore())
                .ForMember(dest => dest.Addresses, opt => opt.Ignore())
                .ForMember(dest => dest.Payments, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore());
        }
    }
} 