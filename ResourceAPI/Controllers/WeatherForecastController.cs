using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using System.ComponentModel;

namespace ResourceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize]
    [RequiredScope("api1")]
    public class WeatherForecastController : ControllerBase
    {
        private bool _IsMaintenance = false;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return _IsMaintenance
                ? Enumerable.Empty<WeatherForecast>()
                : Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                }).ToArray();
        }

        [HttpPost]
        public void Maintain([FromQuery] bool isFinish)
        {
            _IsMaintenance = !isFinish;
        }
    }
}