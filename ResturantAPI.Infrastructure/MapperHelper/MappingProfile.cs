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
    class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerProfileDTO>()
                .ForMember(dest=>dest.UserName,src=>src.MapFrom(src=>src.User.Name))
                .ForMember(dest=>dest.Email,src=>src.MapFrom(src=>src.User.Email))
                .ForMember(dest => dest.PhoneNumber, src => src.MapFrom(src => src.User.PhoneNumber));
        }
    }
}
