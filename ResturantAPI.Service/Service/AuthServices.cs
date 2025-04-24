using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ResturantAPI.Domain.Entities;
using ResturantAPI.Services.Dtos;
using ResturantAPI.Services.Enums;
using ResturantAPI.Services.IService;
using ResturantAPI.Services.Model;
 using ResturantAPI.Services.AuthHelper;
namespace ResturantAPI.Services.Service
{
    public class AuthServices(UserManager<ApplicationUser> userManager,IConfiguration config) : IAuthServices
    {
        private readonly UserManager<ApplicationUser> userManager = userManager;
        private readonly IConfiguration config = config;

        public async Task<Response<bool>> Register(RegisterDTO registerDTO)
        {
            // Check if the user already exists
            var existingUser = await userManager.FindByEmailAsync(registerDTO.Email);
            if (existingUser != null)
            {
                return new Response<bool>
                {
                    Data = false,
                    Status = ResponseStatus.Conflict,
                    Message = "User already exists"
                };
            }
            // Create a new user
            var newUser = new ApplicationUser
            {
                UserName = registerDTO.Email,
                Email = registerDTO.Email,
                Name = registerDTO.Name,
                PhoneNumber = registerDTO.PhoneNumber,
            };
            var result = await userManager.CreateAsync(newUser, registerDTO.Password);
            if (result.Succeeded)
            {
                return new Response<bool>
                {
                    Data = true,
                    Status = ResponseStatus.Success,
                    Message = "User registered successfully"
                };
            }
            else
            {
                return new Response<bool>
                {
                    Data = false,
                    Status = ResponseStatus.BadRequest,
                    Message = "User registration failed",
                    InternalMessage = string.Join(", ", result.Errors.Select(e => e.Description))
                };
            }
        }

        public async Task<Response<LoginResponseDTO>> Login(LoginDTO loginDTO)
        {
            try
            {
                // Check if the user exists
                ApplicationUser user = await userManager.FindByEmailAsync(loginDTO.Email);
                if (user == null)
                {
                    return new Response<LoginResponseDTO>
                    {
                        Data = null,
                        Status = ResponseStatus.NotFound,
                        Message = "User not found"
                    };
                }
                // Check if the password is correct
                bool result = await userManager.CheckPasswordAsync(user, loginDTO.Password);
                if (result)
                {

                    if (user == null)
                    {
                        return new Response<LoginResponseDTO>
                        {
                            Data = null,
                            Status = ResponseStatus.NotFound,
                            Message = "User not found"
                        };
                    }
                    string token = await userManager.GenerateTokenAsync(user, config);
                    return new Response<LoginResponseDTO>
                    {
                        Data = new LoginResponseDTO
                        {
                            Token = token,
                            Name = user.Name,
                            Email = user.Email,
                            PhoneNumber = user.PhoneNumber
                        },
                        Status = ResponseStatus.Success,
                        Message = "Login Success"
                    };
                }

                return new Response<LoginResponseDTO>
                {
                    Data = null,
                    Status = ResponseStatus.BadRequest,
                    Message = "Invalid Eamil Or password",

                };
            }
            catch(Exception ex)
            {
                return new Response<LoginResponseDTO>
                {
                    Data = null,
                    Status = ResponseStatus.InternalServerError,
                    Message = "An error occurred during login",
                    InternalMessage = ex.Message
                };
            }


        }
        public Task<string> RefreshToken(string token, string refreshToken)
        {
            throw new NotImplementedException();
        }
        public Task<bool> RevokeToken(string token)
        {
            throw new NotImplementedException();
        }
    }
    
}
