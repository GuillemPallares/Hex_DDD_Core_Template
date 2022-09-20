using Hex_DDD_Core_Template.Core.Domain.Common;

namespace Hex_DDD_Core_Template.Core.Domain.WeatherAggregate
{
    public interface IWeatherRepository : IRepository<WeatherForecast>
    {
        Task<IEnumerable<WeatherForecast>> GetForecastsAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<WeatherForecast> GetForecastByDateAsync(DateTime date, CancellationToken cancellationToken = default(CancellationToken));
    }
}