using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ResturantAPI.Services.Dtos;
using ResturantAPI.Services.Model;

namespace ResturantAPI.Services.IService
{

    public interface IAuthServices
    {
        Task<Response<bool>> Register(RegisterDTO registerDTO);
        Task<Response<LoginResponseDTO>> Login(LoginDTO loginDTO);
        Task<Response<string>> RefreshToken(string oldToken, string refreshToken);

        Task<Response<bool>> ConfirmEmailUseingOTP(string userId, int otp);

        Task<Response<bool>> ForgetPasswordAsync(string Email);

        Task<Response<bool>> ResetPasswordAsync(ResetPasswordDTO restPasswordDTO);

        Task<Response<bool>> ChangePasswordAsync(string UserId, string oldPassword, string NewPassword);

        Task<Response<UserProfileDTO>> GetUserProfileAsync(string userId);
        Task<Response<bool>> UpdateUserProfileAsync(string userId, UpdateUserProfileDTO profileDTO);
        Task<Response<bool>> UpdateUserProfileImageAsync(string userId, string imageUrl);

        Task<string?> GetCurrentUserId();


    }

}
