using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Net;
using System.Net.Http.Headers;
using Water.Management.Application.Interfaces;
using Water.Management.Shared.Models;
using Water.Management.Shared.ViewModels;

namespace Water.Management.Client.Pages.Geophysic;

public partial class Update
{
    [Inject] private NavigationManager NavigationManager { get; set; } = default!;
    [Inject] private IToastService ToastService { get; set; } = default!;
    [Inject] private IGeophysicsService GeophysicsService { get; set; } = default!;
    [Inject] private IRegionService RegionService { get; set; } = default!;
    [Parameter] public Guid GeophysicsId { get; set; }

    private Geophysics Model { get; set; } = new();
    private IReadOnlyList<IBrowserFile>? Files { get; set; }
    private readonly long maxFileSize = 5120000;
    private bool isLoading = true;
    private bool isLoadingButton = false;

    private GeophysicsVm? geophysics;
    private List<ProvinceVm> provinces = new();
    private List<CityVm> cities = new();
    private List<DistrictVm> districts = new();
    private List<SubDistrictVm> subDistricts = new();

    protected async override Task OnInitializedAsync()
    {
        try
        {
            geophysics = await GeophysicsService.GetByIdAsync(GeophysicsId);
            Model = new Geophysics(geophysics);
            provinces = await RegionService.GetAllProvinceAsync();

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

    private async Task HandleFormUpdate()
    {
        try
        {
            isLoadingButton = true;

            using var content = new MultipartFormDataContent
            {
                { new StringContent(Model.Code!), "Payload.Code" },
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

            if (Model.Aquifer.HasValue)
                content.Add(new StringContent(Model.Aquifer.ToString()!), "Payload.Aquifer");

            if (Model.Elevation.HasValue)
                content.Add(new StringContent(Model.Elevation.ToString()!), "Payload.Elevation");

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

            await GeophysicsService.UpdateAsync(GeophysicsId, content);
            isLoadingButton = false;

            NavigationManager.NavigateTo($"/geophysics/detail/{GeophysicsId}");
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
        NavigationManager.NavigateTo($"/geophysics/detail/{GeophysicsId}");
    }
}