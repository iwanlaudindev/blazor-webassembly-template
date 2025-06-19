using Shared.Wrapper;
using System.Net.Http.Json;
using System.Text;
using Water.Management.Application.Constants;
using Water.Management.Application.Extensions;
using Water.Management.Application.Interfaces;
using Water.Management.Shared.Models;
using Water.Management.Shared.Queries;
using Water.Management.Shared.Utils;
using Water.Management.Shared.ViewModels;

namespace Water.Management.Application.Services;

public class WaterSourceService : IWaterSourceService
{
    private readonly IHttpClientFactory _httpClient;

    public WaterSourceService(IHttpClientFactory httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task CreateAsync(MultipartFormDataContent dataContent)
    {
        HttpResponseMessage response = await _httpClient.PostAsFormAsync(ApiEndpoint.CreateWaterSupply, dataContent);
        await response.HandleErrorResponseAsync();
    }

    public async Task<PagedList<WaterSourceVm>> GetAllAsync(WaterSourceQuery query)
    {
        var queries = query.GetStringBuilder();

        if (!string.IsNullOrWhiteSpace(query.Status))
            queries = (queries.Length > 0) ? queries.Append($"&status={query.Status}") : queries.Append($"?status={query.Status}");

        if (!string.IsNullOrWhiteSpace(query.ProvinceId))
            queries = (queries.Length > 0) ? queries.Append($"&provinceId={query.ProvinceId}") : queries.Append($"?provinceId={query.ProvinceId}");

        if (!string.IsNullOrWhiteSpace(query.CityId))
            queries = (queries.Length > 0) ? queries.Append($"&cityId={query.CityId}") : queries.Append($"?cityId={query.CityId}");

        if (query.From.HasValue && query.To.HasValue)
        {
            queries = (queries.Length > 0) ? queries.Append($"&from={query.From}") : queries.Append($"?from={query.From}");
            queries = (queries.Length > 0) ? queries.Append($"&to={query.To}") : queries.Append($"?to={query.To}");
        }

        HttpResponseMessage response = await _httpClient.GetAsync($"{ApiEndpoint.GetAllWaterSupply}{queries}");
        await response.HandleErrorResponseAsync();

        var result = await response.Content.ReadFromJsonAsync<PagedList<WaterSourceVm>>();
        return await Task.FromResult(result!);
    }

    public async Task<WaterSourceVm> GetByIdAsync(Guid waterSupplyId)
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"{ApiEndpoint.GetWaterSupplyById}{waterSupplyId}");
        await response.HandleErrorResponseAsync();

        var result = await response.Content.ReadFromJsonAsync<WaterSourceVm>();
        return await Task.FromResult(result!);
    }

    public async Task<List<WaterConditionVm>> GetAllWaterConditionAsync()
    {
        HttpResponseMessage response = await _httpClient.GetAsync(ApiEndpoint.GetAllWaterSupplyCondition);
        await response.HandleErrorResponseAsync();

        var result = await response.Content.ReadFromJsonAsync<List<WaterConditionVm>>();
        return await Task.FromResult(result!);
    }

    public async Task<List<WaterTypeVm>> GetAllWaterTypeAsync()
    {
        HttpResponseMessage response = await _httpClient.GetAsync(ApiEndpoint.GetAllWaterSupplyType);
        await response.HandleErrorResponseAsync();

        var result = await response.Content.ReadFromJsonAsync<List<WaterTypeVm>>();
        return await Task.FromResult(result!);
    }

    public async Task UpdateAsync(Guid waterSupplyId, MultipartFormDataContent dataContent)
    {
        HttpResponseMessage response = await _httpClient.PutAsFormAsync($"{ApiEndpoint.UpdateWaterSupply}{waterSupplyId}", dataContent);
        await response.HandleErrorResponseAsync();
    }

    public async Task SetApprovalAsync(Guid waterSupplyId, Approval approval)
    {
        HttpResponseMessage response = await _httpClient.PatchAsync($"{ApiEndpoint.SetApprovalStatus}{waterSupplyId}", approval);
        await response.HandleErrorResponseAsync();
    }

    public async Task<List<WaterSourceVm>> GetAllReportAsync(DateOnly? from, DateOnly? to)
    {
        StringBuilder query = new();

        if (from.HasValue && to.HasValue)
            query = query.Append($"?from={from}&to={to}");

        HttpResponseMessage response = await _httpClient.GetAsync($"{ApiEndpoint.GetAllWaterSupplyReport}{query}");
        await response.HandleErrorResponseAsync();

        var result = await response.Content.ReadFromJsonAsync<List<WaterSourceVm>>();
        return await Task.FromResult(result!);
    }
}
