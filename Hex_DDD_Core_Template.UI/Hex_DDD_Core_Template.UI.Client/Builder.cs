using Hex_DDD_Core_Template.UI.Client.Services;

public static class Builder
{

    public static IServiceCollection AddHex_DDD_Core_TemplateUI(this IServiceCollection services)
    {
        services.AddRazorPages();
        services.AddServerSideBlazor();
        services.AddHex_DDD_Core_TemplateBuilderHttpClients();
        services.AddDirectoryBrowser();
        services.AddSingleton<WeatherForecastService>();

        return services;
    }

    public static WebApplication UseHex_DDD_Core_TemplateUI(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseDirectoryBrowser("/Directory");

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseRouting();

        app.MapBlazorHub();
        app.MapFallbackToPage("/_Host");

        return app;
    }

}

public static class HttpClientBuilder
{
    public static IServiceCollection AddHex_DDD_Core_TemplateBuilderHttpClients(this IServiceCollection services)
    {
        services.AddHWeatherForecastHttpClient();
        //services.AddApplicationDbContext(configuration);

        return services;
    }

    public static IServiceCollection AddHWeatherForecastHttpClient(this IServiceCollection services)
    {
        services.AddHttpClient("WeatherForecast", httpClient =>
        {
            httpClient.BaseAddress = new Uri("https://localhost:7112/");

            // using Microsoft.Net.Http.Headers;
            // The GitHub API requires two headers.
            //httpClient.DefaultRequestHeaders.Add(
            //    HeaderNames.Accept, "application/vnd.github.v3+json");
            //httpClient.DefaultRequestHeaders.Add(
            //    HeaderNames.UserAgent, "HttpRequestsSample");
        });

        return services;
    }

}
