using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantAPI.Services.Dtos
{
    public class UserWithRoleDTO
    {
        public string UserName { get; set; }
        public List<string> Roles  { get; set; }
    }
    public class UserWithRoleResponseDTO
    {
        public string Token { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public string RefreshToken { get; set; }
    }
}
