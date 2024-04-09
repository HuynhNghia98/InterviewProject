using GroupManagement.Models.Response;
using Microsoft.AspNetCore.Mvc;

namespace GroupManagement.Services.Weather.Interfaces
{
	public interface IWeatherService
	{
		Task<ApiResponse<Models.WeatherInfo>> GetWeather();
	}
}
