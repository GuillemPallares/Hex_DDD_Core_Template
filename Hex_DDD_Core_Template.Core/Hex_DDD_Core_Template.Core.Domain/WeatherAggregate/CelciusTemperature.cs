using Hex_DDD_Core_Template.Core.Domain.Common;

namespace Hex_DDD_Core_Template.Core.Domain.WeatherAggregate
{
    public class CelciusTemperature : ValueObject
    {
        public double Value {get; private set;}

        public CelciusTemperature(double value)
        {
            Value = value;
        }

        public static CelciusTemperature FromFarenheitTemperature(double value)
            => new CelciusTemperature((value * 0.5556) - 32);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}