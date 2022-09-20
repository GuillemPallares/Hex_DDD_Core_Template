
using Hex_DDD_Core_Template.Core.Domain.WeatherAggregate;
using Hex_DDD_Core_Template.Core.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Hex_DDD_Core_Template.Core.Infrastructure.Builders
{
    public static partial class Hex_DDD_Core_TemplateBuilder
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public static IServiceCollection AddHex_DDD_Core_TemplateContexts(this IServiceCollection services)
        {
            services.AddSqliteApplicationDbContext();
            return services;
        }

        public static IServiceCollection AddInMemoryApplicationDbContext(this IServiceCollection services)
        {
            //var sqlConnection = Configuration.GetSection(ConnectionBuilder.Position).Get<ConnectionBuilder>();

            services.AddDbContext<ApplicationDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("InMemoryQuestion");
                    });

            return services;
        }

        public static IServiceCollection AddSqliteApplicationDbContext(this IServiceCollection services)
        {
            //var sqlConnection = Configuration.GetSection(ConnectionBuilder.Position).Get<ConnectionBuilder>();

            services.AddDbContext<ApplicationDbContext>(options =>
                    {
                        options.UseSqlite($"Data Source=Test.sqlite", b => b.MigrationsAssembly("Hex_DDD_Core_Template.Core.Infrastructure"));
                    });

            return services;
        }

        public static WebApplication UseHex_DDD_Core_TemplateContexts(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var dataContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dataContext.Database.Migrate();
                
                if(dataContext.WeatherForecasts.Count() == 0)
                {
                    dataContext.WeatherForecasts.AddRange(
                        Enumerable.Range(1, 5).Select(index => 
                            new WeatherForecast(
                                DateTime.Now.AddDays(index),
                                new CelciusTemperature(Random.Shared.Next(-20, 55)),
                                Summaries[Random.Shared.Next(Summaries.Length)])
                            )
                    );

                    dataContext.SaveChanges();
                }
            }
            return app;
        }
    }
}