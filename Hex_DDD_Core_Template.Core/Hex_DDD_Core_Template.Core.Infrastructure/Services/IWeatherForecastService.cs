using Hex_DDD_Core_Template.Core.Domain.WeatherAggregate;

namespace Hex_DDD_Core_Template.Core.Infrastructure.Services
{
    public interface IWeatherForecastService<R>
    {
        Task<R[]> GetForecasts();
        Task<R> GetForecastByDate(DateTime startDate);
    }
}