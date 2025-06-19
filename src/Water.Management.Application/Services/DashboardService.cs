using System.Net.Http;
using System.Net.Http.Json;
using Water.Management.Application.Constants;
using Water.Management.Application.Extensions;
using Water.Management.Application.Interfaces;
using Water.Management.Shared.ViewModels;

namespace Water.Management.Application.Services;

public class DashboardService : IDashboardService
{
    private readonly IHttpClientFactory _httpClient;

    public DashboardService(IHttpClientFactory httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<DashboardSummaryVm> GetSummary()
    {
        HttpResponseMessage response = await _httpClient.GetAsync(ApiEndpoint.Summary);
        await response.HandleErrorResponseAsync();

        var result = await response.Content.ReadFromJsonAsync<DashboardSummaryVm>();
        return await Task.FromResult(result!);
    }

    public async Task<List<WaterSourceVm>> GetWaterSupplyPendingStatus()
    {
        HttpResponseMessage response = await _httpClient.GetAsync(ApiEndpoint.GetWaterSourcePendingStatus);
        await response.HandleErrorResponseAsync();

        var result = await response.Content.ReadFromJsonAsync<List<WaterSourceVm>>();
        return await Task.FromResult(result!);
    }
}
