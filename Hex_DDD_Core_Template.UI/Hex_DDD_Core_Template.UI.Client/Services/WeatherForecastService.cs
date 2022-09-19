using Hex_DDD_Core_Template.UI.Client.Models;

namespace Hex_DDD_Core_Template.UI.Client.Services
{
    public class WeatherForecastService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private readonly HttpClient _httpClient;

        public WeatherForecastService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _httpClient = _httpClientFactory.CreateClient("WeatherForecast");
        }

        public async Task<WeatherForecastModel[]> GetForecasts()
        {
            var httpResponseMessage = await _httpClient.GetAsync($"/GetForecasts");

            httpResponseMessage.EnsureSuccessStatusCode();
            return await httpResponseMessage.Content.ReadFromJsonAsync<WeatherForecastModel[]>(); ;
        }

        public Task<WeatherForecastModel> GetForecastByDate(DateTime startDate)
        {
            throw new NotImplementedException();
        }
    }
}