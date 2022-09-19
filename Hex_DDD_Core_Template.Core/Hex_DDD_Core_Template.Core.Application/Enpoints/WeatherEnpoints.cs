using Hex_DDD_Core_Template.Core.Application.Responses;
using Hex_DDD_Core_Template.Core.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Hex_DDD_Core_Template.Core.Application.Enpoints
{
    public static class WeatherEnpoints
    {
        public static IEndpointRouteBuilder AddWeatherEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapGet("/GetForecasts", async ([FromServices]IWeatherForecastService<WeatherForecastResponse> ws) => {
                return Results.Ok(await ws.GetForecasts());
            });

            app.MapGet("/GetForecastByDate/{date}", async ([FromServices]IWeatherForecastService<WeatherForecastResponse> ws, DateTime date) => {
                 return Results.Ok(await ws.GetForecastByDate(date));
            });

            return app;
        }
    }
}