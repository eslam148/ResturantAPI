using ResturantAPI.Domain;
using ResturantAPI.Services.Dtos;
using ResturantAPI.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 
using System.Threading.Tasks;

namespace ResturantAPI.Services.IService
{
    public interface IAdminServices
    {
        Task<Response<AdminResponseDTO>> AddRoleToUser(AdminDTO adminDTO);
        Task<IEnumerable<UserWithRoleDTO>> GetAllUsersWithRolesAsync();
        Task<Response<PagedResult<UserDto>>> ReportAboutUsers(string FilterByRole = null, int pageSize = 10);
    }
}
