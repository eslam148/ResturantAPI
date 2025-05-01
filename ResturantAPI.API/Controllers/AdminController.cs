using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using ResturantAPI.Domain;
using ResturantAPI.Domain.Entities;
using ResturantAPI.Services.Dtos;
using ResturantAPI.Services.Enums;
using ResturantAPI.Services.IService;
using ResturantAPI.Services.Model;
using System.Security.Claims;

namespace ResturantAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IAdminServices adminServices;

        public AdminController(UserManager<ApplicationUser> userManager, IAdminServices adminServices)
        {
            this.userManager = userManager;
            this.adminServices = adminServices;
        }

        [HttpPost]
        [Route("AddRoleToUser")]
        //[Authorize]
        public async Task<Response<AdminResponseDTO>> AddRoleToUser(AdminDTO adminDTO)
        {

            return await adminServices.AddRoleToUser(adminDTO);
        }

        [HttpPut]
        [Route("UpdateRole")]
        public ActionResult UpdateRole(AdminDTO adminDTO)
        {

            return Ok();
        }

        [HttpGet]
        [Route("GetAllUsersWithRolesAsync")]
        //[Authorize]
        public async Task<IEnumerable<UserWithRoleDTO>> GetAllUsersWithRolesAsync()
        {
            return await adminServices.GetAllUsersWithRolesAsync();
        }

        [HttpPost]
        [Route("GenerateAdminReport")]

        public async Task<Response<PagedResult<UserDto>>> GenerateAdminReport(string FilterByRole = null, int pageSize = 10)
        {
            return await adminServices.GenerateAdminReport(FilterByRole, pageSize);
        }
    }
}
