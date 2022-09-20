using Hex_DDD_Core_Template.UI.Models;
using Microsoft.AspNetCore.Components;

namespace Hex_DDD_Core_Template.UI.Components;

public partial class WeatherForecastListComponent
{
    [Parameter]
    public WeatherForecastModel[] WeatherForecasts { get; set; }
}