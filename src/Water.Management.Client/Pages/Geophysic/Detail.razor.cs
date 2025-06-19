using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Water.Management.Application.Interfaces;
using Water.Management.Shared.Models;
using Water.Management.Shared.ViewModels;

namespace Water.Management.Client.Pages.Geophysic;

public partial class Detail
{
    [Inject] public NavigationManager NavigationManager { get; set; } = default!;
    [Inject] public IGeophysicsService GeophysicsService { get; set; } = default!;
    [Inject] public IToastService ToastService { get; set; } = default!;
    [Parameter] public Guid GeophysicsId { get; set; }

    private GeophysicsVm? geophysics;
    private bool isLoading = true;
    private bool isApproved = false;
    private string? message;
    private string? title;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            geophysics = await GeophysicsService.GetByIdAsync(GeophysicsId);
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
            await GeophysicsService.SetApprovalAsync(GeophysicsId, approval);
            isApproved = true;
            title = approved ? "Accepted!" : "Rejected!";
            message = approved ? "Successfully Accepted!" : "Successfully Rejected!";

            try
            {
                geophysics = await GeophysicsService.GetByIdAsync(GeophysicsId);
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

    private void HandleClickButttonEdit(Guid? GeophysicsId)
    {
        NavigationManager.NavigateTo($"/geophysics/edit/{GeophysicsId}");
    }
}