using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace com.app.testing.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly Settings _options;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IOptionsSnapshot<Settings> options)
        {
            _logger = logger;
            _options = options.Value;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IActionResult Get()
        {
            return Ok(_options);
        }
    }

    public class Settings
    {
        public string Env { get; set; }
        public string Keyvault { get; set; }
    }
    public class Keyvault
    {
        public string UserId { get; set; }
    }

}
