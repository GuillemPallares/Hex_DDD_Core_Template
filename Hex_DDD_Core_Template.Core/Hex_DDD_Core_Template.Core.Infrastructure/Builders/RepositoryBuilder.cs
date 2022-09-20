
using Hex_DDD_Core_Template.Core.Domain.WeatherAggregate;
using Hex_DDD_Core_Template.Core.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Hex_DDD_Core_Template.Core.Infrastructure.Builders
{
    public static partial class Hex_DDD_Core_TemplateBuilder
    {
        public static IServiceCollection AddHex_DDD_Core_TemplateRepositories(this IServiceCollection services)
        {
            services.AddTransient<IWeatherRepository, WeatherRepository>();

            return services;
        }
    }
}