using Hex_DDD_Core_Template.UI.Models;

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
            var httpResponseMessage = await _httpClient.GetAsync($"api/Weather/GetForecasts");

            httpResponseMessage.EnsureSuccessStatusCode();
            return await httpResponseMessage.Content.ReadFromJsonAsync<WeatherForecastModel[]>();
        }

        public async Task<WeatherForecastModel> GetForecastByDate(DateTime startDate)
        {
            var httpResponseMessage = await _httpClient.GetAsync($"api/Weather/GetForecastByDate?Date={startDate.Ticks}");

            httpResponseMessage.EnsureSuccessStatusCode();
            return await httpResponseMessage.Content.ReadFromJsonAsync<WeatherForecastModel>();
        }
    }
}