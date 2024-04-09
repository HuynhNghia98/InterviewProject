namespace GroupManagement.Models
{
	public class WeatherInfo
	{
		public int cityId { get; set; }
		public string cityName { get; set; }
		public string weatherMain { get; set; }
		public string weatherDescription { get; set; }
		public string weatherIcon { get; set; }
		public float mainTemp { get; set; }
		public int mainHumidity { get; set; }
	}
}
