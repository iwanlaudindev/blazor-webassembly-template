using Blazored.Toast.Services;
using Water.Management.Application.Interfaces;
using Water.Management.Shared.Models;
using Water.Management.Shared.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Water.Management.Client.Pages.Users;

public partial class Create
{
    [Inject] private NavigationManager NavigationManager { get; set; } = default!;
    [Inject] private IUserService UserService { get; set; } = default!;
    [Inject] private IToastService ToastService { get; set; } = default!;

    private User User { get; set; } = new User();
    private bool isLoadingButton = false;

    private List<RoleVm> RolesVm { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        try
        {
            RolesVm = await UserService.GetAllUserRolesAsync();
        }
        catch (HttpRequestException ex)
        {
            ToastService.ShowError(ex.Message);
        }
    }

    private async Task HandleUserCreate()
    {
        try
        {
            isLoadingButton = true;

            await UserService.CreateUserAsync(User);
            isLoadingButton = false;

            NavigationManager.NavigateTo("/users");
        }
        catch (HttpRequestException ex)
        {
            isLoadingButton = false;
            ToastService.ShowError(ex.Message);
        }
    }

    private void HandleButtonCancel()
    {
        NavigationManager.NavigateTo("/users");
    }
}