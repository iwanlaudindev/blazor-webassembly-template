using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Json;
using Water.Management.Application.Constants;
using Water.Management.Application.Extensions;
using Water.Management.Application.Interfaces;
using Water.Management.Shared.Queries;
using Water.Management.Shared.ViewModels;

namespace Water.Management.Client.Pages.Gis;

public partial class Index : IAsyncDisposable
{
    [Inject] private IJSRuntime JsRuntime { get; set; } = default!;
    [Inject] private IToastService ToastService { get; set; } = default!;
    [Inject] private IGisService GisService { get; set; } = default!;

    private IJSObjectReference? module;
    private bool isLoading = true;
    private CoordinateTypeQuery Query { get; set; } = new();
    private List<CoordinateVm> coordinateData = new();

    protected override async Task OnInitializedAsync()
    {
        await LoadData();
        isLoading = false;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await InitializeMapAsync();
        }
    }

    private async Task LoadData()
    {
        try
        {
            coordinateData = new();

            if (Query.IsNotEmptyWaterSource)
            {
                var waterCoordinate = await GisService.GetWaterCoordinateAsync();
                if (waterCoordinate is not null)
                    coordinateData.AddRange(waterCoordinate);
            }
            
            if (Query.IsNotEmptyGeophysics)
            {
                var geophysicsCoordinate = await GisService.GetGeophysicsCoorinateAsync();
                if (geophysicsCoordinate is not null)
                    coordinateData.AddRange(geophysicsCoordinate);
            }

            StateHasChanged();
        }
        catch (Exception ex)
        {
            ToastService.ShowError(ex.Message);
        }
    }

    private async void ToggleWaterSource()
    {
        Query.SetIsNotEmptyWaterSource();
        await LoadData();
        await InitializeMapAsync();
    }

    private async void ToggleGeophysics()
    {
        Query.SetIsNotEmptyGeophysics();
        await LoadData();
        await InitializeMapAsync();
    }

    private async Task InitializeMapAsync()
    {
        try
        {
            
            module = await JsRuntime.InvokeAsync<IJSObjectReference>("import", "./js/map.js");
            await module.InvokeVoidAsync("initMap", coordinateData);
        }
        catch (Exception ex)
        {
            ToastService.ShowError(ex.Message);
        }
    }

    [JSInvokable]
    public static async Task<WaterSourceVm> ShowDetailWaterSourceAsync(string waterSupplyId)
    {
        if(Guid.TryParse(waterSupplyId, out Guid id))
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri("https://waterdata-api-grg0addebxfmcrdp.southeastasia-01.azurewebsites.net")
            };
            var request = new HttpRequestMessage(HttpMethod.Get, $"{ApiEndpoint.GetDetailWaterSupply}{id}");
            var response = await client.SendAsync(request);
            await response.HandleErrorResponseAsync();

            var result = await response.Content.ReadFromJsonAsync<WaterSourceVm>();
            return await Task.FromResult(result!);
        }
        return await Task.FromResult(new WaterSourceVm());
    }

    [JSInvokable]
    public static async Task<GeophysicsVm> ShowDetailGeophysicsAsync(string waterSupplyId)
    {
        if (Guid.TryParse(waterSupplyId, out Guid id))
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri("https://waterdata-api-grg0addebxfmcrdp.southeastasia-01.azurewebsites.net")
            };
            var request = new HttpRequestMessage(HttpMethod.Get, $"{ApiEndpoint.GetDetailGeophysics}{id}");
            var response = await client.SendAsync(request);
            await response.HandleErrorResponseAsync();

            var result = await response.Content.ReadFromJsonAsync<GeophysicsVm>();
            return await Task.FromResult(result!);
        }
        return await Task.FromResult(new GeophysicsVm());
    }

    async ValueTask IAsyncDisposable.DisposeAsync()
    {
        if (module is not null)
        {
            await module.DisposeAsync();
        }
    }
}