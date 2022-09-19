using Hex_DDD_Core_Template.Core.Application.Responses;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSwag;
using Hex_DDD_Core_Template.Core.Domain.WeatherAggregate;
using Hex_DDD_Core_Template.Core.Infrastructure.Repositories;
using FastEndpoints;
using FastEndpoints.Swagger;

namespace Hex_DDD_Core_Template.Core.Application.Builders
{
    public static class Hex_DDD_Core_TemplateBuilder
    {
        public static IServiceCollection AddHex_DDD_Core_Template(this IServiceCollection services)
        {
            services.AddTransient<IWeatherRepository, WeatherRepository>();
            services.AddAutoMapper(typeof(Hex_DDD_Core_TemplateBuilder));
            services.AddEndpointsApiExplorer();
            services.AddSwaggerDoc(shortSchemaNames: true, settings: s =>
            {
                s.DocumentName = "Initial Release";
                s.Title = "my api";
                s.Version = "v1.0";
            });
            services.AddFastEndpoints();

            return services;
        }

        public static WebApplication UseHex_DDD_Core_Template(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseOpenApi();
                app.UseSwaggerUi3(s => s.ConfigureDefaults());
                app.UseReDoc(s => s.Path = "/docs");
            }


            app.UseHttpsRedirection();

            app.UseFastEndpoints(c =>
            {
                c.Endpoints.RoutePrefix = "api";
            });
            
            return app;
        }
    }
}