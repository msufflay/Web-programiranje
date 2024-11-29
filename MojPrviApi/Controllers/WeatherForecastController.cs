using Microsoft.AspNetCore.Mvc;
using MojPrviApi.Models;

namespace MojPrviApi.Controllers;

[ApiController]
// [Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
    User user = new User{
        Id = 1,
        Name = "Marko",
        Surname = "Horvat"
    };
    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet("api/dohvati-tempvri")]
    public IEnumerable<WeatherForecast> DohvatiTemperaturu()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
    [HttpGet("api/get-user")]
    public User getUser(){
        return this.user;
    }
}
