using Water.Management.Application.Constants;
using Water.Management.Application.Extensions;
using Water.Management.Application.Interfaces;
using Water.Management.Shared.ViewModels;
using System.Net.Http.Json;

namespace Water.Management.Application.Services;

public class GisService : IGisService
{
    private readonly IHttpClientFactory _httpClient;

    public GisService(IHttpClientFactory httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<CoordinateVm>> GetWaterCoordinateAsync()
    {
        HttpResponseMessage response = await _httpClient.GetAsync(ApiEndpoint.GetAllWaterSupplyCoordinate);
        await response.HandleErrorResponseAsync();

        var result = await response.Content.ReadFromJsonAsync<List<CoordinateVm>>();
        return await Task.FromResult(result!);
    }

    public async Task<WaterSourceVm> GetDetailWaterSupplyAsync(Guid waterSupplyId)
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"{ApiEndpoint.GetDetailWaterSupply}{waterSupplyId}");
        await response.HandleErrorResponseAsync();

        var result = await response.Content.ReadFromJsonAsync<WaterSourceVm>();
        return await Task.FromResult(result!);
    }

    public async Task<List<CoordinateVm>> GetGeophysicsCoorinateAsync()
    {
        HttpResponseMessage response = await _httpClient.GetAsync(ApiEndpoint.GetAllGeophysicsCoordinate);
        await response.HandleErrorResponseAsync();

        var result = await response.Content.ReadFromJsonAsync<List<CoordinateVm>>();
        return await Task.FromResult(result!);
    }
}

