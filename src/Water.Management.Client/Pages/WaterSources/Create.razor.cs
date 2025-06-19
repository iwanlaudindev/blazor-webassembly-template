using Blazored.Toast.Services;
using Water.Management.Application.Interfaces;
using Water.Management.Shared.Models;
using Water.Management.Shared.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Headers;
using System.Net;
using Microsoft.AspNetCore.Components.Forms;

namespace Water.Management.Client.Pages.WaterSources;

public partial class Create : IAsyncDisposable
{
    [Inject] private NavigationManager NavigationManager { get; set; } = default!;
    [Inject] private IJSRuntime JsRuntime { get; set; } = default!;
    [Inject] private IToastService ToastService { get; set; } = default!;
    [Inject] private IWaterSourceService WaterSourceService { get; set; } = default!;
    [Inject] private IRegionService RegionService { get; set; } = default!;

    private IJSObjectReference? module;
    private WaterSource Model { get; set; } = new();
    private IReadOnlyList<IBrowserFile>? Files { get; set; }
    private readonly long maxFileSize = 5120000;
    private bool isLoading = true;
    private bool isLoadingButton = false;

    private List<WaterTypeVm> types = new();
    private List<ProvinceVm> provinces = new();
    private List<CityVm> cities = new();
    private List<DistrictVm> districts = new();
    private List<SubDistrictVm> subDistricts = new();
    private List<WaterConditionVm> conditions = new();

    protected async override Task OnInitializedAsync()
    {
        try
        {
            provinces = await RegionService.GetAllProvinceAsync();
            types = await WaterSourceService.GetAllWaterTypeAsync();
            conditions = await WaterSourceService.GetAllWaterConditionAsync();
            isLoading = false;
        }
        catch (Exception ex)
        {
            ToastService.ShowError(ex.Message);
        }
    }
    private async Task HandleWaterSupplyCreate()
    {
        try
        {
            isLoadingButton = true;

            using var content = new MultipartFormDataContent
            {
                { new StringContent(Model.Code!), nameof(Model.Code) },
                { new StringContent(Model.Name!), nameof(Model.Name) },
                { new StringContent(Model.SocialMapping!), nameof(Model.SocialMapping) },
                { new StringContent(Model.Rainfall!), nameof(Model.Rainfall) },
                { new StringContent(Model.Demography!), nameof(Model.Demography) },
                { new StringContent(Model.Vegetation!), nameof(Model.Vegetation) },
                { new StringContent(Model.GeneralGeology!), nameof(Model.GeneralGeology) },
                { new StringContent(Model.StartSurveyDt?.ToString("yyyy-MM-dd")!), nameof(Model.StartSurveyDt) },
                { new StringContent(Model.EndSurveyDt?.ToString("yyyy-MM-dd")!), nameof(Model.EndSurveyDt) },
                { new StringContent(Model.Latitude.ToString()!), nameof(Model.Latitude) },
                { new StringContent(Model.Longitude.ToString()!), nameof(Model.Longitude) },
                { new StringContent(Model.ProvinceId!), nameof(Model.ProvinceId) },
                { new StringContent(Model.CityId!), nameof(Model.CityId) },
                { new StringContent(Model.DistrictId!), nameof(Model.DistrictId) },
                { new StringContent(Model.SubDistrictId!), nameof(Model.SubDistrictId) }
            };

            if (Model.Ph.HasValue)
                content.Add(new StringContent(Model.Ph.ToString()!), nameof(Model.Ph));

            if (Model.TDSPPM.HasValue)
                content.Add(new StringContent(Model.TDSPPM.ToString()!), nameof(Model.TDSPPM));

            if (Model.Depth.HasValue)
                content.Add(new StringContent(Model.Depth.ToString()!), nameof(Model.Depth));

            if (Model.WaterTable.HasValue)
                content.Add(new StringContent(Model.WaterTable.ToString()!), nameof(Model.WaterTable));

            if (Model.Elevation.HasValue)
                content.Add(new StringContent(Model.Elevation.ToString()!), nameof(Model.Elevation));

            if (!string.IsNullOrWhiteSpace(Model.WaterSupplyTypeId))
                content.Add(new StringContent(Model.WaterSupplyTypeId), nameof(Model.WaterSupplyTypeId));

            if (!string.IsNullOrWhiteSpace(Model.TypeOther))
                content.Add(new StringContent(Model.TypeOther), nameof(Model.TypeOther));

            if (Model.Conditions is not null)
                foreach (var item in Model.Conditions)
                {
                    content.Add(new StringContent(item), nameof(Model.Conditions));
                }

            if (!string.IsNullOrWhiteSpace(Model.ConditionOther))
                content.Add(new StringContent(Model.ConditionOther), nameof(Model.ConditionOther));

            if (!string.IsNullOrWhiteSpace(Model.DetailLocation))
                content.Add(new StringContent(Model.DetailLocation), nameof(Model.DetailLocation));

            if (Files is not null)
                foreach (var file in Files)
                {
                    string safeFileName = WebUtility.HtmlEncode(file.Name);
                    var fileContent = new StreamContent(file.OpenReadStream(maxFileSize));
                    fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse(file.ContentType);
                    content.Add(fileContent, "Images", safeFileName);
                }

            await WaterSourceService.CreateAsync(content);
            isLoadingButton = false;

            NavigationManager.NavigateTo("/water-source");
        }
        catch (Exception ex)
        {
            isLoadingButton = false;
            ToastService.ShowError(ex.Message);
        }
    }

    private void OnInputFileChange(InputFileChangeEventArgs e)
    {
        Files = e.GetMultipleFiles();
    }

    private async Task OnAfterProvinceSelectedAsync()
    {
        try
        {
            if(!string.IsNullOrWhiteSpace(Model.ProvinceId))
            {
                cities = await RegionService.GetAllCityAsync(Model.ProvinceId);

                Model.CityId = null;
                districts = new List<DistrictVm>();
                Model.DistrictId = null;
                subDistricts = new List<SubDistrictVm>();
                Model.SubDistrictId = null;
                
            }
        }
        catch (Exception ex)
        {
            ToastService.ShowError(ex.Message);
        }
    }

    private async Task OnAfterRegencySelectedAsync()
    {
        try
        {
            if (!string.IsNullOrWhiteSpace(Model.CityId))
            {
                districts = await RegionService.GetAllDistrictAsync(Model.CityId);

                Model.DistrictId = null;
                subDistricts = new List<SubDistrictVm>();
                Model.SubDistrictId = null;
            }
        }
        catch (Exception ex)
        {
            ToastService.ShowError(ex.Message);
        }
    }

    private async Task OnAfterDistrictSelectedAsync()
    {
        try
        {
            if (!string.IsNullOrWhiteSpace(Model.DistrictId))
            {
                subDistricts = await RegionService.GetAllSubDistrictAsync(Model.DistrictId);

                Model.SubDistrictId = null;
            }
        }
        catch (Exception ex)
        {
            ToastService.ShowError(ex.Message);
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            module = await JsRuntime.InvokeAsync<IJSObjectReference>("import", "./js/map.js");
            var location = await RetrieveLocation();
            if (location.Length > 0)
            {
                Model.Latitude = location[0];
                Model.Longitude = location[1];

                StateHasChanged();
            }

        }
    }

    private async Task<double[]> RetrieveLocation()
    {
        if (module is not null)
            return await module.InvokeAsync<double[]>("getCurrentPosition");

        return Array.Empty<double>();
    }

    private void HandleButtonCancel()
    {
        NavigationManager.NavigateTo("/water-source");
    }

    async ValueTask IAsyncDisposable.DisposeAsync()
    {
        if (module is not null)
        {
            await module.DisposeAsync();
        }
    }
}