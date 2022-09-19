using Hex_DDD_Core_Template.Core.Application.Responses;
using Hex_DDD_Core_Template.Core.Domain.WeatherAggregate;
using FastEndpoints;

namespace Hex_DDD_Core_Template.Core.Application.Handlers
{
    public class GetWeatherForecastsEndpoint : EndpointWithoutRequest<IEnumerable<WeatherForecastResponse>>
    {
        private readonly IWeatherRepository _weatherRepository;

        private readonly AutoMapper.IMapper _mapper;

        public override void Configure()
        {
            Get("/Weather/GetForecasts");
            AllowAnonymous();
            //Version(1);
            //Options(x => x.WithTags("","Answers"));
        }

        public GetWeatherForecastsEndpoint(IWeatherRepository weatherRepository, AutoMapper.IMapper mapper)
        {
            _weatherRepository = weatherRepository;
            _mapper = mapper;
        }

        public override async Task HandleAsync(CancellationToken c)
            => await SendAsync( _mapper.Map<IEnumerable<WeatherForecastResponse>>(await _weatherRepository.GetForecastsAsync()));
    }
}