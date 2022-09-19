using Hex_DDD_Core_Template.Core.Domain.Common;

namespace Hex_DDD_Core_Template.Core.Domain.WeatherAggregate
{
    public class FarenheitTemperature : ValueObject
    {
        public double Value {get; private set;}

        public FarenheitTemperature(double value)
        {
            Value = value;
        }

        public static FarenheitTemperature FromCelciusTemperature(double value)
            => new FarenheitTemperature( 32 + (value / 0.5556));

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}