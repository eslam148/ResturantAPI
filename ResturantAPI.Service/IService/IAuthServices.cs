using System;
using System.Collections.Generic;
using System.Linq;
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

        Task<Response<bool>> ResetPasswordAsync(string Email, string password, string token);

        Task<Response<bool>> ChangePasswordAsync(ChangePasswordDTO changePassword);

        Task<Response<object>> GetUserProfileAsync(string userId);
        Task<Response<bool>> UpdateUserProfileAsync(string userId, string name, string phoneNumber, string email);
        Task<Response<bool>> UpdateUserProfileImageAsync(string userId, string imageUrl);




    }
}
