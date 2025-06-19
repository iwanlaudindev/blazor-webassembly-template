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

public class GeophysicsService : IGeophysicsService
{
    private readonly IHttpClientFactory _httpClient;

    public GeophysicsService(IHttpClientFactory httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task CreateAsync(MultipartFormDataContent dataContent)
    {
        HttpResponseMessage response = await _httpClient.PostAsFormAsync(ApiEndpoint.CreateGeophysics, dataContent);
        await response.HandleErrorResponseAsync();
    }

    public async Task<PagedList<GeophysicsVm>> GetAllAsync(GeophisicsQuery query)
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

        HttpResponseMessage response = await _httpClient.GetAsync($"{ApiEndpoint.GetAllGeophysics}{queries}");
        await response.HandleErrorResponseAsync();

        var result = await response.Content.ReadFromJsonAsync<PagedList<GeophysicsVm>>();
        return await Task.FromResult(result!);
    }

    public async Task<List<GeophysicsVm>> GetAllReportAsync(DateOnly? from, DateOnly? to)
    {
        StringBuilder query = new();

        if (from.HasValue && to.HasValue)
            query = query.Append($"?from={from}&to={to}");

        HttpResponseMessage response = await _httpClient.GetAsync($"{ApiEndpoint.GetAllGeophysicsReport}{query}");
        await response.HandleErrorResponseAsync();

        var result = await response.Content.ReadFromJsonAsync<List<GeophysicsVm>>();
        return await Task.FromResult(result!);
    }

    public async Task<GeophysicsVm> GetByIdAsync(Guid geophysicsaId)
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"{ApiEndpoint.GetByIdGeophysics}{geophysicsaId}");
        await response.HandleErrorResponseAsync();

        var result = await response.Content.ReadFromJsonAsync<GeophysicsVm>();
        return await Task.FromResult(result!);
    }

    public async Task SetApprovalAsync(Guid geophysicsaId, Approval approval)
    {
        HttpResponseMessage response = await _httpClient.PatchAsync($"{ApiEndpoint.SetApprovalStatusGeophysics}{geophysicsaId}", approval);
        await response.HandleErrorResponseAsync();
    }

    public async Task UpdateAsync(Guid geophysicsaId, MultipartFormDataContent dataContent)
    {
        HttpResponseMessage response = await _httpClient.PutAsFormAsync($"{ApiEndpoint.UpdateGeophysics}{geophysicsaId}", dataContent);
        await response.HandleErrorResponseAsync();
    }
}
