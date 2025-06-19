using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Water.Management.Application.Interfaces;
using Water.Management.Shared.ViewModels;

namespace Water.Management.Client.Pages;

public partial class Index
{
    [Inject] private IToastService ToastService { get; set; } = default!;
    [Inject] private IDashboardService DashboardService { get; set; } = default!;

    private bool isLoading = true;

    private DashboardSummaryVm? summary;
    private List<WaterSourceVm>? waterSources;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            await LoadData();
            isLoading = false;
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
            summary = await DashboardService.GetSummary();
            waterSources = await DashboardService.GetWaterSupplyPendingStatus();
        }
        catch (Exception ex)
        {
            ToastService.ShowError(ex.Message);
        }
    }
}