using Blazored.LocalStorage;
using Blazored.Toast;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Water.Management.Application.Services;
using Water.Management.Application.Comman;
using Water.Management.Application.Interfaces;

namespace Water.Management.Application;

public static class ServiceCollection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services,
        ApplicationSettings applicationSettings)
    {
        // blazored services
        services.AddBlazoredToast();
        services.AddBlazoredLocalStorage();

        // authetication & authorization
        services.AddOptions();
        services.AddAuthorizationCore();
        services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
        services.AddTransient<CustomHttpClientHandler>();

        // configuring http clients
        services.AddHttpClient("komodoWaterApiV1", client =>
        {
            client.BaseAddress = new Uri(applicationSettings.BaseAddress);
        }).AddHttpMessageHandler<CustomHttpClientHandler>();

        // use case services
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IDashboardService, DashboardService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IWaterSourceService, WaterSourceService>();
        services.AddScoped<IGeophysicsService, GeophysicsService>();
        services.AddScoped<IRegionService, RegionService>();
        services.AddScoped<IGisService, GisService>();

        // js runtime service
        services.AddScoped<IJSRuntimeService, JSRuntimeService>();

        return services;
    }
}

