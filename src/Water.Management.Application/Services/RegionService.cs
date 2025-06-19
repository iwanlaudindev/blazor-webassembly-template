using Water.Management.Application.Constants;
using Water.Management.Application.Extensions;
using Water.Management.Application.Interfaces;
using Water.Management.Shared.ViewModels;
using System.Net.Http.Json;

namespace Water.Management.Application.Services;

public class RegionService : IRegionService
{
    private readonly IHttpClientFactory _httpClient;

    public RegionService(IHttpClientFactory httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<ProvinceVm>> GetAllProvinceAsync()
    {
        HttpResponseMessage response = await _httpClient.GetAsync(ApiEndpoint.GetAllProvince);
        await response.HandleErrorResponseAsync();

        var result = await response.Content.ReadFromJsonAsync<List<ProvinceVm>>();
        return await Task.FromResult(result!);
    }

    public async Task<List<CityVm>> GetAllCityAsync(string provinceId)
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"{ApiEndpoint.GetAllRegency}?provinceId={provinceId}");
        await response.HandleErrorResponseAsync();

        var result = await response.Content.ReadFromJsonAsync<List<CityVm>>();
        return await Task.FromResult(result!);
    }

    public async Task<List<DistrictVm>> GetAllDistrictAsync(string cityId)
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"{ApiEndpoint.GetAllDistrict}?cityId={cityId}");
        await response.HandleErrorResponseAsync();

        var result = await response.Content.ReadFromJsonAsync<List<DistrictVm>>();
        return await Task.FromResult(result!);
    }

    public async Task<List<SubDistrictVm>> GetAllSubDistrictAsync(string districtId)
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"{ApiEndpoint.GetAllVillage}?districtId={districtId}");
        await response.HandleErrorResponseAsync();

        var result = await response.Content.ReadFromJsonAsync<List<SubDistrictVm>>();
        return await Task.FromResult(result!);
    }
}
