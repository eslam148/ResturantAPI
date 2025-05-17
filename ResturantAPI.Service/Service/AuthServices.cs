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
using ResturantAPI.Domain.Interface;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
namespace ResturantAPI.Services.Service
{
    public class AuthServices(UserManager<ApplicationUser> userManager,IConfiguration config,IUnitOfWork unitOfWork , IHttpContextAccessor httpContextAccessor) : IAuthServices
    {
        private readonly UserManager<ApplicationUser> userManager = userManager;
        private readonly IConfiguration config = config;
 
        public IUnitOfWork UnitOfWork { get; } = unitOfWork;

        public async Task<Response<bool>> Register(RegisterDTO registerDTO)
        {
            // Check if the user already exists
            if (await userManager.FindByEmailAsync(registerDTO.Email) is not null)
            {
                return new Response<bool>
                {
                    Data = false,
                    Status = ResponseStatus.Conflict,
                    Message = "User already exists"
                };
            }

            var newUser = new ApplicationUser
            {
                UserName = registerDTO.Email,
                Email = registerDTO.Email,
                Name = registerDTO.Name,
                PhoneNumber = registerDTO.PhoneNumber,
            };

            var creationResult = await userManager.CreateAsync(newUser, registerDTO.Password);
            if (!creationResult.Succeeded)
            {
                return new Response<bool>
                {
                    Data = false,
                    Status = ResponseStatus.BadRequest,
                    Message = "User registration failed",
                    InternalMessage = string.Join(", ", creationResult.Errors.Select(e => e.Description))
                };
            }

            var roleResult = await userManager.AddToRoleAsync(newUser, registerDTO.role.ToString());
            if (!roleResult.Succeeded)
            {
                await userManager.DeleteAsync(newUser);
                return new Response<bool>
                {
                    Data = false,
                    Status = ResponseStatus.BadRequest,
                    Message = "User registration failed",
                    InternalMessage = string.Join(", ", roleResult.Errors.Select(e => e.Description))
                };
            }

            await AddRoleSpecificEntityAsync(newUser.Id, registerDTO.role);

            var saveResult = await UnitOfWork.SaveAsync();
            if (saveResult > 0)
            {
                await userManager.GenerateOTPAsync(newUser.Id);

                return new Response<bool>
                {
                    Data = true,
                    Status = ResponseStatus.Success,
                    Message = "User registered successfully"
                };
               
            }

            await userManager.DeleteAsync(newUser);
            return new Response<bool>
            {
                Data = false,
                Status = ResponseStatus.BadRequest,
                Message = "User registration failed",
                InternalMessage = "Failed to save user data"
            };
        }

        private async Task AddRoleSpecificEntityAsync(string userId, Role role)
        {
            switch (role)
            {
                case Role.Customer:
                    await UnitOfWork.Customer.AddAsync(new Customer { UserId = userId });
                    break;
                case Role.Delivery:
                    await UnitOfWork.Delivery.AddAsync(new Delivery { UserId = userId });
                    break;
                case Role.Restaurant:
                    await UnitOfWork.Restaurant.AddAsync(new Restaurant { UserId = userId });
                    break;
            }
        }

        public async Task<Response<bool>> ConfirmEmail(string userId, string token)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new Response<bool>
                {
                    Data = false,
                    Status = ResponseStatus.NotFound,
                    Message = "User not found"
                };
            }
            var result = await userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return new Response<bool>
                {
                    Data = true,
                    Status = ResponseStatus.Success,
                    Message = "Email confirmed successfully"
                };
            }
            return new Response<bool>
            {
                Data = false,
                Status = ResponseStatus.BadRequest,
                Message = "Email confirmation failed",
                InternalMessage = string.Join(", ", result.Errors.Select(e => e.Description))
            };
        }
        public async Task<Response<LoginResponseDTO>> Login(LoginDTO loginDTO)
        {
            try
            {
                 
                // Check if the user exists
                ApplicationUser? user = await userManager.FindByEmailAsync(loginDTO.Email);
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
                    string token = await userManager.GenerateTokenAsync(user);
                    string refreshToken = await  userManager.GenerateRefreshTokenAsync(user);
                    return new Response<LoginResponseDTO>
                    {
                        Data = new LoginResponseDTO
                        {
                            Token = token,
                            RefreshToken = refreshToken,
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
        public async Task<Response<string>> RefreshToken(string oldToken, string refreshToken)
        {
            try
            {
                string? token = await userManager.RefreshToken(oldToken, refreshToken);

                if (token == null)
                {
                    return new Response<string>
                    {
                        Data = null,
                        Status = ResponseStatus.BadRequest,
                        Message = "Invalid refresh token or token expired"
                    };
                }

                return new Response<string>
                {
                    Data = token,
                    Status = ResponseStatus.Success,
                    Message = "Token refreshed successfully"
                };
            }
            catch (Exception ex)
            {
                // Optional: You can log the exception here

                return new Response<string>
                {
                    Data = null,
                    Status = ResponseStatus.InternalServerError,
                    Message = $"An error occurred while refreshing the token: {ex.Message}"
                };
            }
        }

        public async Task<Response<int>> GenerateOTP(string userId)
        {
            
            return new Response<int>
                  {
                      Data = await userManager.GenerateOTPAsync(userId),
                      Status = ResponseStatus.BadRequest,
                      Message = "User registration failed",
                      InternalMessage = "Failed to save user data"
                  };
        }

        public async Task<Response<bool>> ConfirmEmailUseingOTP(string userId,int otp)
        {
            try
            {
                bool result = await userManager.ConfirmEmailOtp(userId, otp);
                if (result)
                {
                    return new Response<bool>
                    {
                        Data = result,
                        Status = ResponseStatus.Success,
                        Message = "Email confirmed successfully"
                    };
                }
                else
                {
                    return new Response<bool>
                    {
                        Data = result,
                        Status = ResponseStatus.BadRequest,
                        Message = "Invalid OTP or OTP expired"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response<bool>
                {
                    Data = false,
                    Status = ResponseStatus.InternalServerError,
                    Message = "An error occurred during OTP confirmation",
                    InternalMessage = ex.Message
                };
            }


        }

        public async Task<Response<bool>> ForgetPasswordAsync(string Email)
        {
            try
            {
              ApplicationUser? user = await  userManager.FindByEmailAsync(Email);
                if(user == null)
                {
                    return Response<bool>.Fail("User Not Found");
                }
                string token =   await  userManager.GeneratePasswordResetTokenAsync(user);
                return Response<bool>.Success(true, token);
            } catch (Exception ex) {
                return Response<bool>.Error("", ex.Message);
            }
        }

        public async Task<Response<bool>> ResetPasswordAsync(ResetPasswordDTO restPasswordDTO)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(restPasswordDTO.Email);
                if (user == null)
                {
                    return new Response<bool>
                    {
                        Data = false,
                        Status = ResponseStatus.NotFound,
                        Message = "User not found"
                    };
                }
             //   string token = await userManager.GeneratePasswordResetTokenAsync(user);
                IdentityResult result = await userManager.ResetPasswordAsync(user, restPasswordDTO.OTP, restPasswordDTO.Password);
                if (result.Succeeded)
                {
                    return new Response<bool>
                    {
                        Data = true,
                        Status = ResponseStatus.Success,
                        Message = "Password reset successfully"
                    };
                }
                return new Response<bool>
                {
                    Data = false,
                    Status = ResponseStatus.BadRequest,
                    Message = "Password reset failed",
                    InternalMessage = string.Join(", ", result.Errors.Select(e => e.Description))
                };
            }
            catch (Exception ex)
            {
                return new Response<bool>
                {
                    Data = false,
                    Status = ResponseStatus.InternalServerError,
                    Message = "An error occurred during password reset",
                    InternalMessage = ex.Message
                };
            }


        }

        public async Task<Response<bool>> ChangePasswordAsync(string UserId,string oldPassword,string NewPassword )
        {
            try
            {
                 ApplicationUser? user = await userManager.FindByIdAsync(UserId);
                IdentityResult result = await userManager.ChangePasswordAsync(user, oldPassword, NewPassword);
                if (result.Succeeded)
                {
                    return new Response<bool>
                    {
                        Data = true,
                        Status = ResponseStatus.Success,
                        Message = "Password changed successfully"
                    };
                }
                else
                {
                    return new Response<bool>
                    {
                        Data = false,
                        Status = ResponseStatus.BadRequest,
                        Message = "Password change failed",
                        InternalMessage = string.Join(", ", result.Errors.Select(e => e.Description))
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response<bool>
                {
                    Data = false,
                    Status = ResponseStatus.InternalServerError,
                    Message = "An error occurred during password change",
                    InternalMessage = ex.Message
                };
            }

        }

        public async Task<Response<UserProfileDTO>> GetUserProfileAsync(string userId)
        {
            ApplicationUser? user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return  new Response<UserProfileDTO>
                {
                    Data = null,
                    Status = ResponseStatus.NotFound,
                    Message = "User not found"
                };
            }
            var userProfile = new UserProfileDTO
            {
               Name = user.Name,
               Email = user.Email,
              PhoneNumber =  user.PhoneNumber,
             
            };
            return  new Response<UserProfileDTO>
            {
                Data = userProfile,
                Status = ResponseStatus.Success,
                Message = "User profile retrieved successfully"
            };
        }

        public async Task<Response<bool>> UpdateUserProfileAsync(string userId, UpdateUserProfileDTO userProfileDTO)
        {

            try
            {
                ApplicationUser? user = await userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return new Response<bool>
                    {
                        Data = false,
                        Status = ResponseStatus.NotFound,
                        Message = "User not found"
                    };
                }
                user.Name = userProfileDTO.Name;
                user.PhoneNumber = userProfileDTO.PhoneNumber;
                user.Email = userProfileDTO.Email;
                var result = await userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return new Response<bool>
                    {
                        Data = true,
                        Status = ResponseStatus.Success,
                        Message = "User profile updated successfully"
                    };
                }
                return new Response<bool>
                {
                    Data = false,
                    Status = ResponseStatus.BadRequest,
                    Message = "User profile update failed",
                    InternalMessage = string.Join(", ", result.Errors.Select(e => e.Description))
                };
            }
            catch (Exception ex)
            {
                return new Response<bool>
                {
                    Data = false,
                    Status = ResponseStatus.InternalServerError,
                    Message = "An error occurred during user profile update",
                    InternalMessage = ex.Message
                };
            }

        }
        public async Task<string?> GetCurrentUserId()
        {
            ClaimsPrincipal? principal = httpContextAccessor.HttpContext?.User;
            if (principal?.Identity?.Name == null)
                return null;

            ApplicationUser? user = await userManager.FindByNameAsync(principal.Identity.Name);
            return user?.Id;
        }
        public Task<Response<bool>> UpdateUserProfileImageAsync(string userId, string imageUrl)
        {
            throw new NotImplementedException();
        }

        public Task<Response<bool>> ResetPasswordAsync()
        {
            throw new NotImplementedException();
        }

        

        
    }
    
}
