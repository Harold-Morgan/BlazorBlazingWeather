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
            //If I call this method asynchronously it'll return null. Problem on the package side?
            Task getWeatherCityWOCountry = Task.Run(async () => { data = await forecast.GetWeatherDataByCityNameAsync(key, "moscow", "ru", WeatherUnits.Metric); });
            getWeatherCityWOCountry.Wait();

            return data;
        }
    }
}
