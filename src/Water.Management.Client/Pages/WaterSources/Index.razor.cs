using Blazored.Toast.Services;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Shared.Wrapper;
using System.Data;
using System.Net.Http.Headers;
using Water.Management.Application.Interfaces;
using Water.Management.Shared.Queries;
using Water.Management.Shared.ViewModels;

namespace Water.Management.Client.Pages.WaterSources;

public partial class Index
{
    [Inject] private IJSRuntime JSRuntime { get; set; } = default!;
    [Inject] private IToastService ToastService { get; set; } = default!;
    [Inject] private IWaterSourceService WaterSourceService { get; set; } = default!;
    [Inject] private IRegionService RegionService { get; set; } = default!;

    private IJSObjectReference? module;

    private WaterSourceQuery Query { get; set; } = new();
    private DateOnly? From { get; set; }
    private DateOnly? To { get; set; }

    private bool isLoading = true;
    private bool isDownloadComplated = false;

    private List<ProvinceVm> provinces = new();
    private List<CityVm> cities = new();

    private PagedList<WaterSourceVm>? waterSources;
    private List<WaterSourceVm>? waterSourceReports;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            await LoadData();
            provinces = await RegionService.GetAllProvinceAsync();
            isLoading = false;
        }
        catch(Exception ex)
        {
            ToastService.ShowError(ex.Message);
        }
    }

    private async Task OnPageChange(int newPage)
    {
        Query.Page = newPage;
        await LoadData();

        if (!(waterSources is not null && waterSources.Items!.Count > 0))
        {
            ToastService.ShowWarning("Data is empty on this page.");

            Query.Page -= 1;
            await LoadData();
        }

        StateHasChanged();
    }

    private async Task OnSearch(string s)
    {
        Query.Search = s;
        Query.Page = 1;
        await LoadData();
    }

    private async Task OnSubmitFilter()
    {
        Query.Page = 1;
        await LoadData();
    }

    private async Task OnClickResetFilter()
    {
        Query = new WaterSourceQuery();
        await LoadData();
    }

    private async Task OnAfterProvinceSelectedAsync()
    {
        try
        {
            if (!string.IsNullOrWhiteSpace(Query.ProvinceId))
            {
                cities = await RegionService.GetAllCityAsync(Query.ProvinceId);

                Query.CityId = null;

            }
        }
        catch (Exception ex)
        {
            ToastService.ShowError(ex.Message);
        }
    }

    private async Task LoadData()
    {
        try
        {
            waterSources = await WaterSourceService.GetAllAsync(Query);
        }
        catch (Exception ex)
        {
            ToastService.ShowError(ex.Message);
        }
    }

    private async Task LoadReportData()
    {
        try
        {
            waterSourceReports = await WaterSourceService.GetAllReportAsync(From, To);
        }
        catch (Exception ex)
        {
            ToastService.ShowError(ex.Message);
        }
    }

    public async void ExportToExcel()
    {
        try
        {
            isDownloadComplated = true;
            await LoadReportData();
            await DownloadFileExcel();
            From = null;
            To = null;
            isDownloadComplated = false;

            StateHasChanged();
        }
        catch(Exception ex)
        {
            ToastService.ShowError(ex.ToString());
        }
    }

    private async Task DownloadFileExcel()
    {
        try
        {
            byte[] excelData = DataToByte(waterSourceReports);
            string fileName = $"waterSource-{DateTime.Now:yyyyMMddHHmmss}.xlsx";
            var content = new ByteArrayContent(excelData);
            content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = fileName
            };

            content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

            module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./js/site.js");
            await module.InvokeVoidAsync(
                "saveAsFile",
                fileName,
                Convert.ToBase64String(excelData),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
        catch (Exception ex)
        {
            ToastService.ShowError($"Download failed. {ex}");
        }
    }

    private static byte[] DataToByte(List<WaterSourceVm>? datas)
    { 
        using var workbook = new XLWorkbook();
        var dataTable = new DataTable();
        dataTable.Columns.Add(new DataColumn("ID"));
        dataTable.Columns.Add(new DataColumn("Name"));
        dataTable.Columns.Add(new DataColumn("Ph"));
        dataTable.Columns.Add(new DataColumn("TDS_ppm"));
        dataTable.Columns.Add(new DataColumn("Depth"));
        dataTable.Columns.Add(new DataColumn("WaterTable"));
        dataTable.Columns.Add(new DataColumn("Elevation"));
        dataTable.Columns.Add(new DataColumn("Type"));
        dataTable.Columns.Add(new DataColumn("Other Type"));
        dataTable.Columns.Add(new DataColumn("Condition"));
        dataTable.Columns.Add(new DataColumn("Other Condition"));
        dataTable.Columns.Add(new DataColumn("Province"));
        dataTable.Columns.Add(new DataColumn("City"));
        dataTable.Columns.Add(new DataColumn("District"));
        dataTable.Columns.Add(new DataColumn("Village"));
        dataTable.Columns.Add(new DataColumn("Detail Location"));
        dataTable.Columns.Add(new DataColumn("Survey Date"));
        dataTable.Columns.Add(new DataColumn("Coordinate"));

        if (datas is not null)
            foreach (var item in datas)
            {
                var newRow = dataTable.NewRow();
                newRow["ID"] = item.Code;
                newRow["Name"] = item.Name;
                newRow["Ph"] = item.Ph;
                newRow["TDS_ppm"] = item.TDSPPM;
                newRow["Depth"] = item.Depth;
                newRow["WaterTable"] = item.WaterTable;
                newRow["Elevation"] = item.Elevation;
                newRow["Type"] = item.Type;
                newRow["Other Type"] = item.TypeOther;
                newRow["Condition"] = string.Join(", ", item.Conditions?.Select(e => e.Name) ?? new List<string>());
                newRow["Other Condition"] = item.ConditionOther;
                newRow["Province"] = item.Province;
                newRow["City"] = item.City;
                newRow["District"] = item.District;
                newRow["Village"] = item.SubDistrict;
                newRow["Detail Location"] = item.DetailLocation;
                newRow["Survey Date"] = $"{item.StartSurveyDt} - {item.EndSurveyDt}";
                newRow["Coordinate"] = $"{item.Latitude}, {item.Longitude}";

                dataTable.Rows.Add(newRow);
            };

        var workSheet = workbook.Worksheets.Add(dataTable, "Water Source");

        using MemoryStream memoryStream = new();
        workbook.SaveAs(memoryStream);
        return memoryStream.ToArray();
    }

    public async ValueTask DisposeAsync()
    {
        if (module is not null)
        {
            await module.DisposeAsync();
            GC.SuppressFinalize(this);
        }
    }
}