using Microsoft.Extensions.Configuration;
using OpenWeatherMap.Standard;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorBlazingWeather.Data
{
    public class WeatherForecastService
    {
        public IConfiguration Configuration { get; }
        public WeatherForecastService(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private static string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public Task<WeatherForecast[]> GetForecastAsync(DateTime startDate)
        {
            var rng = new Random();
            return Task.FromResult(Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = startDate.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            }).ToArray());
        }

        public async Task<WeatherData> GetWeatherData()
        {
            string key = Configuration["OpenWeatherApiKey"];
            Forecast forecast = new Forecast();
            WeatherData data = null;
            data = await forecast.GetWeatherDataByZipAsync(key, "32927", "us", WeatherUnits.Metric);
            //getWeather.Wait();

            return data;
        }
    }
}
