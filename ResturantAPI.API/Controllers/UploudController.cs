using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update;
using ResturantAPI.Services.IService;
using ResturantAPI.Services.Model;

namespace ResturantAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploudController(IUploudServices uploudServices) : ControllerBase
    {
        private readonly IUploudServices uploudServices = uploudServices;

        [HttpPost("uploadImage")]
        public async Task<Response<string>> UploadImage(IFormFile file)
        {
           
           return await uploudServices.UploadImageAsync(file);
            
        }
    }
}
