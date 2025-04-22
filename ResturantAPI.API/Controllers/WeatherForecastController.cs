using Microsoft.AspNetCore.Mvc;
using ResturantAPI.Domain.Entities;
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
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        private readonly IMovieService _movieService;

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IMovieService movieService)
        {
            _logger = logger;
            _movieService = movieService;

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
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
