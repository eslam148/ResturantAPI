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
      
        public async Task<Response<bool>> ConfirmEmailotp(string userId,int otp)
        {
            return await authServices.ConfirmEmailUseingOTP(userId,otp);
            
        }
    }
 
}
