using Microsoft.AspNetCore.Mvc;

namespace ServerTest
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController: ControllerBase
    {
        readonly string[] _summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var forecast = await Task.Run(() => {
                return Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                {
                    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = _summaries[Random.Shared.Next(_summaries.Length)]
                }).ToArray();
            });
                
            return Ok(forecast);
        }
    }
}