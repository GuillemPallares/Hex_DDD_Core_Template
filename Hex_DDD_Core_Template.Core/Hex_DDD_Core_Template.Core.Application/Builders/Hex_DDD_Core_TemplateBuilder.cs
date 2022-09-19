using Hex_DDD_Core_Template.Core.Application.Responses;
using Hex_DDD_Core_Template.Core.Application.Services;
using Hex_DDD_Core_Template.Core.Infrastructure.Services;
using Hex_DDD_Core_Template.Core.Application.Enpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSwag;
using Hex_DDD_Core_Template.Core.Domain.WeatherAggregate;
using Hex_DDD_Core_Template.Core.Infrastructure.Repositories;

namespace Hex_DDD_Core_Template.Core.Application.Builders
{
    public static class Hex_DDD_Core_TemplateBuilder
    {
        public static IServiceCollection AddHex_DDD_Core_Template(this IServiceCollection services)
        {
            services.AddTransient<IWeatherForecastService<WeatherForecastResponse>, WeatherForecastService>();
            services.AddTransient<IWeatherRepository, WeatherRepository>();
            services.AddAutoMapper(typeof(Hex_DDD_Core_TemplateBuilder));
            services.AddEndpointsApiExplorer();
            services.AddSwaggerDocument();

            return services;
        }

        public static WebApplication UseHex_DDD_Core_Template(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseOpenApi(); // serve OpenAPI/Swagger documents
                app.UseSwaggerUi3(); // serve Swagger UI
                app.UseReDoc(); // serve ReDoc UI
            }


            app.UseHttpsRedirection();

            app.AddWeatherEndpoint();

            return app;
        }
    }
}