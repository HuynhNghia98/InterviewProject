using GroupManagement.Services.User.Interfaces;
using GroupManagement.Services.Weather.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GroupManagement.Controllers.Weather
{
	[Route("api/[controller]")]
	[ApiController]
	public class WeatherController : ControllerBase
	{
		private readonly IWeatherService _weather;

		public WeatherController(IWeatherService weather)
		{
			_weather = weather;
		}

		[HttpGet("GetWeather")]
		public async Task<IActionResult> GetWeather()
		{
			var result = await _weather.GetWeather();

			if (result.IsSuccess)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}
		}
	}
}
