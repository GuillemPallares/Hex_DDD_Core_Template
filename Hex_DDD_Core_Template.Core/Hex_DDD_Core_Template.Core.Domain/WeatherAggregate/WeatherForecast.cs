using Hex_DDD_Core_Template.Core.Domain.Common;

namespace Hex_DDD_Core_Template.Core.Domain.WeatherAggregate
{
    public sealed class WeatherForecast : Entity, IAggregateRoot
    {
        public DateTime Date { get; private set;}

        public CelciusTemperature TemperatureC { get; private set;}

        public FarenheitTemperature TemperatureF { get; private set;}

        public string? Summary { get; private set;}

        public WeatherForecast(DateTime date, CelciusTemperature temperatureC, string? summary = null)
        {
            this.Date = date;
            this.TemperatureC = temperatureC;
            this.TemperatureF = FarenheitTemperature.FromCelciusTemperature(temperatureC.Value);
            this.Summary = summary;
        }

        public WeatherForecast(DateTime date, FarenheitTemperature temperatureF, string? summary = null)
        {
            this.Date = date;
            this.TemperatureC = CelciusTemperature.FromFarenheitTemperature(temperatureF.Value);
            this.TemperatureF = temperatureF;
            this.Summary = summary;
        }
    }
}