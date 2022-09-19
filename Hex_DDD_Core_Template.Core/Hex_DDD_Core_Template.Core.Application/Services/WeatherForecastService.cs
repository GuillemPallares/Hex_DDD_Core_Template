using System.Net.Http.Json;
using AutoMapper;
using Hex_DDD_Core_Template.Core.Application.Responses;
using Hex_DDD_Core_Template.Core.Domain.WeatherAggregate;
using Hex_DDD_Core_Template.Core.Infrastructure.Services;

namespace Hex_DDD_Core_Template.Core.Application.Services;

public class WeatherForecastService : IWeatherForecastService<WeatherForecastResponse>
{
    private readonly IWeatherRepository _weatherRepository;
    private readonly IMapper _mapper;

    public WeatherForecastService(IWeatherRepository weatherRepository, IMapper mapper)
    {
        _weatherRepository = weatherRepository;
        _mapper = mapper;
    }

    public async Task<WeatherForecastResponse[]> GetForecasts()
        => _mapper.Map<List<WeatherForecastResponse>>(await _weatherRepository.GetForecastsAsync()).ToArray();

    public async Task<WeatherForecastResponse> GetForecastByDate(DateTime startDate)
        => _mapper.Map<WeatherForecastResponse>(await _weatherRepository.GetForecastByDateAsync(startDate));

}
