using Hex_DDD_Core_Template.Core.Application.Requests;
using Hex_DDD_Core_Template.Core.Application.Responses;
using Hex_DDD_Core_Template.Core.Domain.WeatherAggregate;
using FastEndpoints;

namespace Hex_DDD_Core_Template.Core.Application.Handlers
{
    public class GetWeatherForecastByDateEndpoint : Endpoint<GetForecastByDate, WeatherForecastResponse>
    {
        private readonly IWeatherRepository _weatherRepository;

        private readonly AutoMapper.IMapper _mapper;

         public override void Configure()
        {
            Get("/Weather/GetForecastByDate");
            AllowAnonymous();
            //Version(1);
            //Options(x => x.WithTags("","Answers"));
        }

        public GetWeatherForecastByDateEndpoint(IWeatherRepository weatherRepository, AutoMapper.IMapper mapper)
        {
            _weatherRepository = weatherRepository;
            _mapper = mapper;
        }

        public override async Task HandleAsync(GetForecastByDate request, CancellationToken cancellationToken = default(CancellationToken))
            => await SendAsync( _mapper.Map<WeatherForecastResponse>(await _weatherRepository.GetForecastByDateAsync(new DateTime(request.Date), cancellationToken)));
    }
}