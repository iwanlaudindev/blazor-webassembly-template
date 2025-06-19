using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Headers;
using System.Net;
using Water.Management.Application.Interfaces;
using Water.Management.Shared.Models;
using Water.Management.Shared.ViewModels;

namespace Water.Management.Client.Pages.WaterSources;

public partial class Update
{
    [Inject] NavigationManager NavigationManager { get; set; } = default!;
    [Inject] IToastService ToastService { get; set; } = default!;
    [Inject] IWaterSourceService WaterSourceService { get; set; } = default!;
    [Inject] IRegionService RegionService { get; set; } = default!;
    [Parameter] public Guid WaterSourceId { get; set; }

    private WaterSource Model { get; set; } = new();
    private IReadOnlyList<IBrowserFile>? Files { get; set; }
    private readonly long maxFileSize = 5120000;
    private bool isLoading = true;
    private bool isLoadingButton = false;

    public WaterSourceVm waterSource = new();
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
            waterSource = await WaterSourceService.GetByIdAsync(WaterSourceId);
            Model = new WaterSource(waterSource);
            provinces = await RegionService.GetAllProvinceAsync();
            types = await WaterSourceService.GetAllWaterTypeAsync();
            conditions = await WaterSourceService.GetAllWaterConditionAsync();

            if (!string.IsNullOrWhiteSpace(Model.ProvinceId))
            {
                cities = await RegionService.GetAllCityAsync(Model.ProvinceId);
            }

            if (!string.IsNullOrWhiteSpace(Model.CityId))
            {
                districts = await RegionService.GetAllDistrictAsync(Model.CityId);
            }

            if (!string.IsNullOrWhiteSpace(Model.DistrictId))
            {
                subDistricts = await RegionService.GetAllSubDistrictAsync(Model.DistrictId);
            }

            isLoading = false;
        }
        catch (Exception ex)
        {
            ToastService.ShowError(ex.Message);
        }
    }

    private async Task HandleWaterSourceUpdate()
    {
        try
        {
            isLoadingButton = true;

            using var content = new MultipartFormDataContent
            {
                { new StringContent(Model.Code!), "Payload.Code" },
                { new StringContent(Model.Name!), "Payload.Name" },
                { new StringContent(Model.SocialMapping!), "Payload.SocialMapping" },
                { new StringContent(Model.Rainfall!), "Payload.Rainfall" },
                { new StringContent(Model.Demography!), "Payload.Demography" },
                { new StringContent(Model.Vegetation!), "Payload.Vegetation" },
                { new StringContent(Model.GeneralGeology!), "Payload.GeneralGeology" },
                { new StringContent(Model.StartSurveyDt?.ToString("yyyy-MM-dd")!), "Payload.StartSurveyDt" },
                { new StringContent(Model.EndSurveyDt?.ToString("yyyy-MM-dd")!), "Payload.EndSurveyDt" },
                { new StringContent(Model.Latitude.ToString()!), "Payload.Latitude" },
                { new StringContent(Model.Longitude.ToString()!), "Payload.Longitude" },
                { new StringContent(Model.ProvinceId!), "Payload.ProvinceId" },
                { new StringContent(Model.CityId!), "Payload.CityId" },
                { new StringContent(Model.DistrictId!), "Payload.DistrictId" },
                { new StringContent(Model.SubDistrictId!), "Payload.SubDistrictId" }
            };

            if (Model.Ph.HasValue)
                content.Add(new StringContent(Model.Ph.ToString()!), "Payload.Ph");

            if (Model.TDSPPM.HasValue)
                content.Add(new StringContent(Model.TDSPPM.ToString()!), "Payload.TDSPPM");

            if (Model.Depth.HasValue)
                content.Add(new StringContent(Model.Depth.ToString()!), "Payload.Depth");

            if (Model.WaterTable.HasValue)
                content.Add(new StringContent(Model.WaterTable.ToString()!), "Payload.WaterTable");

            if (Model.Elevation.HasValue)
                content.Add(new StringContent(Model.Elevation.ToString()!), "Payload.Elevation");

            if (!string.IsNullOrWhiteSpace(Model.WaterSupplyTypeId))
                content.Add(new StringContent(Model.WaterSupplyTypeId), "Payload.WaterSupplyTypeId");

            if (!string.IsNullOrWhiteSpace(Model.TypeOther))
                content.Add(new StringContent(Model.TypeOther), "Payload.TypeOther");

            if (Model.Conditions is not null)
                foreach (var item in Model.Conditions)
                {
                    content.Add(new StringContent(item), "Payload.Conditions");
                }

            if (!string.IsNullOrWhiteSpace(Model.ConditionOther))
                content.Add(new StringContent(Model.ConditionOther), "Payload.ConditionOther");

            if (!string.IsNullOrWhiteSpace(Model.DetailLocation))
                content.Add(new StringContent(Model.DetailLocation), "Payload.DetailLocation");

            if (Files is not null)
                foreach (var file in Files)
                {
                    string safeFileName = WebUtility.HtmlEncode(file.Name);
                    var fileContent = new StreamContent(file.OpenReadStream(maxFileSize));
                    fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse(file.ContentType);
                    content.Add(fileContent, "Payload.Images", safeFileName);
                }

            await WaterSourceService.UpdateAsync(WaterSourceId, content);
            isLoadingButton = false;

            NavigationManager.NavigateTo($"/water-source/detail/{WaterSourceId}");
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
            if (!string.IsNullOrWhiteSpace(Model.ProvinceId))
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

    private void HandleButtonCancel()
    {
        NavigationManager.NavigateTo($"/water-source/detail/{WaterSourceId}");
    }
}