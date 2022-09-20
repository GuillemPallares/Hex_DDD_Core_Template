using Hex_DDD_Core_Template.Core.Domain.WeatherAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hex_DDD_Core_Template.Core.Infrastructure.EntityTypeConfigurations
{
    public sealed class WeatherForecastEntityTypeConfiguration : IEntityTypeConfiguration<WeatherForecast>
    {
        public void Configure(EntityTypeBuilder<WeatherForecast> answerConfiguration)
        {
            answerConfiguration.HasKey(b => b.Id);

            answerConfiguration.Property(p => p.Id)
                               .ValueGeneratedOnAdd();

            answerConfiguration.Property(p => p.TemperatureC)
                               .HasConversion(x => x.Value,
                                              y => new CelciusTemperature(y));

            answerConfiguration.Property(p => p.TemperatureF)
                               .HasConversion(x => x.Value,
                                              y => new FarenheitTemperature(y));
        }
    }
}