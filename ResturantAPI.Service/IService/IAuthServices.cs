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
        Task<string> RefreshToken(string token, string refreshToken);
        Task<bool> RevokeToken(string token);
    }
}
