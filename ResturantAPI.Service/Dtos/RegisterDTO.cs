using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ResturantAPI.Services.Enums;

namespace ResturantAPI.Services.Dtos
{
    public class RegisterDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string PhoneNumber { get; set; }
        public IFormFile ImageUrl { get; set; }
        public Role role { get; set; } = Role.Customer ;

    }
}
