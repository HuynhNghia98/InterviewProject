using GroupManagement.Models.Response;
using GroupManagement.Services.Weather.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace GroupManagement.Services.Weather
{
	public class WeatherService : IWeatherService
	{
		private readonly string weatherAPIUrl = "http://api.openweathermap.org/data/2.5/group?id=1580578,1581129,1581297,1581188,1587923&units=metric&appid=91b7466cc755db1a94caf6d86a9c788a";

		public async Task<ApiResponse<Models.WeatherInfo>> GetWeather()
		{
			ApiResponse<Models.WeatherInfo> _res = new();

			try
			{
				using (var client = new HttpClient())
				{
					HttpResponseMessage response = await client.GetAsync(weatherAPIUrl);
					if (response.IsSuccessStatusCode)
					{
						string json = await response.Content.ReadAsStringAsync();
						var weatherData = JsonConvert.DeserializeObject<Models.WeatherInfo>(json);
						_res.Result = weatherData;
						_res.Messages = "Current weather information of cities";
						_res.StatusCode = HttpStatusCode.OK;
					}
					else
					{
						_res.IsSuccess = false;
						_res.StatusCode = response.StatusCode;
						_res.Messages = "Failed to fetch weather data";
					}
				}
			}
			catch (Exception ex)
			{
				_res.IsSuccess = false;
				_res.StatusCode = HttpStatusCode.InternalServerError;
				_res.Messages = $"Internal server error: {ex.Message}";
			}

			return _res;
		}
	}
}
