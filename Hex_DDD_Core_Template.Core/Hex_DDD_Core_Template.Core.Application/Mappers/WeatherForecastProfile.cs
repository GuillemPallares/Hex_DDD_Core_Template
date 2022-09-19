using AutoMapper;
using Hex_DDD_Core_Template.Core.Application.Responses;
using Hex_DDD_Core_Template.Core.Domain.WeatherAggregate;

namespace Hex_DDD_Core_Template.Core.Application.Mappers
{
    public class WeatherForecastMapperProfile : Profile
    {
        public WeatherForecastMapperProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<WeatherForecast, WeatherForecastResponse>()
                .ForMember(x => x.TemperatureC, opt => opt.MapFrom(x => x.TemperatureC.Value))
                .ForMember(x => x.TemperatureF, opt => opt.MapFrom(x => x.TemperatureF.Value))
                .ReverseMap();
        }
    }
}