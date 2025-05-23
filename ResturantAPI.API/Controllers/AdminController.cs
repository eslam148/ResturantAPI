﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using ResturantAPI.Domain;
using ResturantAPI.Domain.Entities;
using ResturantAPI.Services.Dtos;
using ResturantAPI.Services.Dtos.ReportDTO;
using ResturantAPI.Services.Dtos.ResturntReportDTO;
using ResturantAPI.Services.Enums;
using ResturantAPI.Services.IService;
using ResturantAPI.Services.Model;
using System.Linq.Expressions;
using System.Security.Claims;

namespace ResturantAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IAdminServices adminServices;
        private readonly IReportServices reportServices;

        public AdminController(UserManager<ApplicationUser> userManager, IAdminServices adminServices, IReportServices reportServices)
        {
            this.userManager = userManager;
            this.adminServices = adminServices;
            this.reportServices = reportServices;
        }

        [HttpPost]
        [Route("AddRoleToUser")]
        //[Authorize]
        public async Task<Response<AdminResponseDTO>> AddRoleToUser(AdminDTO adminDTO)
        {

            return await adminServices.AddRoleToUser(adminDTO);
        }

        /*[HttpPut]
        [Route("UpdateRole")]
        public ActionResult UpdateRole(AdminDTO adminDTO)
        {

            return Ok();
        }*/

        [HttpGet]
        [Route("GetAllUsersWithRolesAsync")]
        //[Authorize]
        public async Task<IEnumerable<UserWithRoleDTO>> GetAllUsersWithRolesAsync()
        {
            return await adminServices.GetAllUsersWithRolesAsync();
        }

        [HttpPost]
        [Route("ReportAboutUsers")]

        public async Task<Response<PagedResult<UserDto>>> ReportAboutUsers(string FilterByRole = null, int pageSize = 10)
        {
            return await adminServices.ReportAboutUsers(FilterByRole, pageSize);
        }

        /*****************---------------***********----------------*****************-------------************----------------************************/

        /*public IQueryable<AllResturantDto> FilterAll()
        {
            //return reportServices.FilterAll();
        }*/








    }
}
