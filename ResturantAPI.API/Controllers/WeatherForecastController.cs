using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ResturantAPI.Domain.Entities;
using ResturantAPI.Services.Dtos;
using ResturantAPI.Services.Enums;
using ResturantAPI.Services.IService;
using ResturantAPI.Services.Model;
using ResturantAPI.Services.Service;

namespace ResturantAPI.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;

       
        private readonly IMovieService _movieService;

        private readonly ILogger<WeatherForecastController> _logger;
 
        public IUploudServices UploudServices { get; }

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IMovieService movieService , IUploudServices uploudServices,RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _movieService = movieService;
            UploudServices = uploudServices;
            _roleManager = roleManager;
        }
        [HttpGet("Get")]
        public ActionResult<Response<List<Movies>>> GetMovies()
        {
            try
            {

                return _movieService.GetAllMovies();
            }
            catch (Exception ex)
            {
                Response<List<Movies>> response = new Response<List<Movies>>
                {
                    Data =null,
                    Status = ResponseStatus.BadRequest,
                    InternalMessage = ex.Message,

                };
                return response;
            }
          
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public  ActionResult<object> Get()
        {

            return "Hello World";
        }


        //[HttpPost(Name = "Uploud")]
        //public async Task<string> Uploud( uploudApi file)
        //{
        //    return await UploudServices.UploadImageToAzureAsync(file.formFilea);
        //}
    }
    public class uploudApi 
    { 
      public   IFormFile formFilea { get; set; }
   
    }
}
