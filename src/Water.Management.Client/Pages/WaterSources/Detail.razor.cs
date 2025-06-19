using Blazored.Toast.Services;
using Water.Management.Application.Interfaces;
using Water.Management.Shared.ViewModels;
using Microsoft.AspNetCore.Components;
using Water.Management.Shared.Models;

namespace Water.Management.Client.Pages.WaterSources;

public partial class Detail
{
    [Inject] public NavigationManager NavigationManager { get; set; } = default!;
    [Inject] public IWaterSourceService WaterSourceService { get; set; } = default!;
    [Inject] public IToastService ToastService { get; set; } = default!;
    [Parameter] public Guid WaterSourceId { get; set; }

    private WaterSourceVm? waterSource;
    private bool isLoading = true;
    private bool isApproved = false;
    private string? message;
    private string? title;


    protected override async Task OnInitializedAsync()
    {
        try
        {
            waterSource = await WaterSourceService.GetByIdAsync(WaterSourceId);
            isLoading = false;
        }
        catch (Exception ex)
        {
            ToastService.ShowError(ex.Message);
        }
    }

    private async Task HandleClickButttonApproval(bool approved)
    {
        try
        {
            var approval = new Approval { Approved = approved };
            await WaterSourceService.SetApprovalAsync(WaterSourceId, approval);
            isApproved = true;
            title = approved ? "Accepted!" : "Rejected!";
            message = approved ? "Successfully Accepted!" : "Successfully Rejected!";

            try
            {
                waterSource = await WaterSourceService.GetByIdAsync(WaterSourceId);
            }
            catch (Exception ex)
            {
                ToastService.ShowError(ex.Message);
            }
        }
        catch (Exception ex)
        {
            ToastService.ShowError(ex.Message);
        }
    }

    private async void HaldleClickOk()
    {
        await Task.Delay(500);
        isApproved = false;
        title = string.Empty;
        message = string.Empty;

        StateHasChanged();
    }

    private void HandleClickButttonEdit(Guid? WaterSourceId)
    {
        NavigationManager.NavigateTo($"/water-source/edit/{WaterSourceId}");
    }
}