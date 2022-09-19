using Hex_DDD_Core_Template.Core.Domain.WeatherAggregate;

namespace Hex_DDD_Core_Template.Core.Infrastructure.Repositories
{
    public class WeatherRepository : IWeatherRepository
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public Task<IEnumerable<WeatherForecast>> GetForecastsAsync()
        {
            return Task.FromResult(Enumerable.Range(1, 5).Select(index => 
                new WeatherForecast(
                    DateTime.Now.AddDays(index),
                    new CelciusTemperature(Random.Shared.Next(-20, 55)),
                    Summaries[Random.Shared.Next(Summaries.Length)])
                )
            );   
        }

        public Task<WeatherForecast> GetForecastByDateAsync(DateTime date)
        {
            return Task.FromResult(
                new WeatherForecast(
                    date,
                    new CelciusTemperature(Random.Shared.Next(-20, 55)),
                    Summaries[Random.Shared.Next(Summaries.Length)])
                );   
        }
    }
}