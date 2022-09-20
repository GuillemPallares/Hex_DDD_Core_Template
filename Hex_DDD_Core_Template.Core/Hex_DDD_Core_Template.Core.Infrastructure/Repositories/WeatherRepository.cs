using Hex_DDD_Core_Template.Core.Domain.WeatherAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Hex_DDD_Core_Template.Core.Infrastructure.Repositories
{
    public class WeatherRepository : Repository<WeatherForecast>, IWeatherRepository
    {

        public WeatherRepository(ApplicationDbContext context, ILogger<Repository<WeatherForecast>> logger) 
            : base(context, logger){}

        public async Task<IEnumerable<WeatherForecast>> GetForecastsAsync(CancellationToken cancellationToken = default(CancellationToken))
            => await dBSet.ToListAsync(cancellationToken);

        public async Task<WeatherForecast> GetForecastByDateAsync(DateTime date, CancellationToken cancellationToken = default(CancellationToken))
            => await dBSet.FirstAsync(x => x.Date == date, cancellationToken);
    }
}