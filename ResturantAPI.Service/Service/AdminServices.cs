using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ResturantAPI.Domain;
using ResturantAPI.Domain.Entities;
using ResturantAPI.Domain.Interface;
using ResturantAPI.Services.Dtos;
using ResturantAPI.Services.Enums;
using ResturantAPI.Services.IService;
using ResturantAPI.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ResturantAPI.Services.Service
{
    public class AdminServices : IAdminServices
    {
        private readonly UserManager<ApplicationUser> userManager;
        //private readonly RoleManager<IdentityRole> identityRole;
        private readonly IUnitOfWork unitOfWork;

        public AdminServices(UserManager<ApplicationUser> userManager/*, RoleManager<IdentityRole> identityRole*/, IUnitOfWork unitOfWork)
        {
            this.userManager = userManager;
            //this.identityRole = identityRole;
            this.unitOfWork = unitOfWork;
        }
        public async Task<Response<AdminResponseDTO>> AddRoleToUser(AdminDTO adminDTO)
        {
            
            var user = await userManager.FindByNameAsync(adminDTO.UserName);
            if (user == null)
            {
                return new Response<AdminResponseDTO>
                {
                    Data = null,
                    Status = ResponseStatus.NotFound,
                    Message = "User not found"
                };
            }


            IdentityResult? result = await userManager.AddToRoleAsync(user, "Admin");
            if (result.Succeeded)
            {
                return new Response<AdminResponseDTO>
                {
                    Status = ResponseStatus.Success,
                    Message = "User added sucsessfuly"
                };
            }
            return new Response<AdminResponseDTO>
            {
                Data = null,
                Status = ResponseStatus.NotFound,
                Message = "User added to role failed"
            };
        }

        public async Task<IEnumerable<UserWithRoleDTO>> GetAllUsersWithRolesAsync()
        {
            List<ApplicationUser> users =  userManager.Users.ToList();
            var result = new List<UserWithRoleDTO>();

            foreach (ApplicationUser user in users)
            {
                IList<string> roles = await userManager.GetRolesAsync(user);
                result.Add(new UserWithRoleDTO
                {
                    UserName = user.UserName,
                    Roles = roles.ToList()
                });
            }

            return result;
        }

        /*public async Task<Response<AdminResponseDTO>> UpdateRole(string userName, AdminDTO adminDTO)
        {
            ApplicationUser? user = await userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return new Response<AdminResponseDTO>
                {
                    Status = ResponseStatus.NotFound,
                    Data = null,
                    Message = "Not Found user with this name"
                };
            }
            int countUserName = await userManager.Users.CountAsync(u => u.UserName == userName);
            if (countUserName == 1)
            {
                return new Response<AdminResponseDTO>
                {
                    Status = ResponseStatus.Conflict,
                    Data = null,
                    Message = "UserName Name is Already found"
                };
            }


            return new Response<AdminResponseDTO>
            {

            };
        }*/

        public async Task<Response<PagedResult<UserDto>>> GenerateAdminReport(string FilterByRole = null , int pageSize = 10)
        {
            IEnumerable<UserDto> users;
            if (string.IsNullOrEmpty(FilterByRole))
            {
                users = userManager.Users.Select( u => new UserDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email
                });
               
            }
            else
            {
                IEnumerable<ApplicationUser> usersWithRoleEntered = await userManager.GetUsersInRoleAsync(FilterByRole);
                users = usersWithRoleEntered.Select(u => new UserDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email
                });
               
            }
            
            var pagenationResult = await GetPaginatedAsync(users, u => u.Name, pageSize);
            var respons = new PagedResult<UserDto>
            {
                Items = pagenationResult.Items,
                TotalCount = pagenationResult.TotalCount,
                PageSize = pagenationResult.PageSize
            };

            return new Response<PagedResult<UserDto>>
            {
                Data = respons,
                Message = "result of filter and send counter",
                Status = ResponseStatus.Success,
            };
        }

        private async Task<PagedResult<UserDto>> GetPaginatedAsync(IEnumerable<UserDto> users, Func<UserDto,object>? order = default, int pageSize = 10, int pageNumber = 1)
        {

            int totalCount = users.Count();

            if (order is not null)
            {
                users = users.OrderBy(order);
            }
            else
            {
                users = users.OrderBy(u => u.Id);
            }


            IEnumerable<UserDto> items = users
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);


            return new PagedResult<UserDto> 
            {
                Items = (IQueryable<UserDto>)items,
                TotalCount = totalCount
            };
        }
    }
}
