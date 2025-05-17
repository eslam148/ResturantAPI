using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResturantAPI.Services.Dtos;
using ResturantAPI.Services.Enums;
using ResturantAPI.Services.IService;
using ResturantAPI.Services.Model;

namespace ResturantAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(IAuthServices authServices) : ControllerBase
    {
        private readonly IAuthServices authServices = authServices;

        [HttpPost("login")]
        public async Task<Response<LoginResponseDTO>> Login(LoginDTO loginDTO)
        {
            return await authServices.Login(loginDTO);
        }


        [HttpPost("register")]
        public async Task<Response<bool>> Register(RegisterDTO registerDTO)
        {
            return await authServices.Register(registerDTO);
        }
        [HttpPost("refresh-token")]
        [Authorize]
        public async Task<Response<string>> RefreshToken(RefreshTokenDto refreshTokenDto)
        {

            return await authServices.RefreshToken(refreshTokenDto.token, refreshTokenDto.refreshToken); ;
        }

        [HttpPost("Confirm-Email-otp")]

        public async Task<Response<bool>> ConfirmEmailotp(string userId, int otp)
        {
            return await authServices.ConfirmEmailUseingOTP(userId, otp);

        }

        [HttpGet("GetUserProfile")]
        [Authorize]
        public async Task<Response<UserProfileDTO>> GetUserProfile()
        {
            string? UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return await authServices.GetUserProfileAsync(UserId);
        }
        [HttpPut("UpdateUserProfile")]
        [Authorize]
        public async Task<Response<bool>> UpdateUserProfile(UpdateUserProfileDTO profileDTO)
        {
            string? UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return await authServices.UpdateUserProfileAsync(UserId, profileDTO);
        }

        [HttpPatch("ChangePassword")]
        [Authorize]
        public async Task<Response<bool>> ChangePassword(ChangePasswordDTO changePasswordDTO)
        {

            string? UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return await authServices.ChangePasswordAsync(UserId, changePasswordDTO.OldPassword, changePasswordDTO.NewPassword);

        }

        [HttpPost("ForgetPassword")]
        public async Task<Response<bool>> ForgotPasswrod(string Email)
        {
            return await authServices.ForgetPasswordAsync(Email);
        }

        [HttpPatch("ResetPasswrod")]
        public async Task<Response<bool>> ResetPasswrod(ResetPasswordDTO restPasswordDTO)
        {
            return await authServices.ResetPasswordAsync(restPasswordDTO);
        }



    }
}
