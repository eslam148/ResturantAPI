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
    }
}
