using Blazored.Toast.Services;
using Water.Management.Application.Interfaces;
using Water.Management.Shared.Models;
using Water.Management.Shared.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Water.Management.Client.Pages.Users;

public partial class Update
{
    [Inject] private NavigationManager NavigationManager { get; set; } = default!;
    [Inject] private IUserService UserService { get; set; } = default!;
    [Inject] private IToastService ToastService { get; set; } = default!;

    [Parameter]
    public Guid? UserId { get; set; }

    private bool isLoadingButton = false;
    private User User { get; set; } = new();
    public UserVm UserVm { get; set; } = new();
    public List<RoleVm> RolesVm { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        try
        {
            if (UserId.HasValue)
            {
                UserVm = await UserService.GetUserByIdAsync((Guid)UserId);
                RolesVm = await UserService.GetAllUserRolesAsync();
                User = new User
                {
                    Username = UserVm.Username,
                    FullName = UserVm.FullName,
                    Email = UserVm.Email,
                    PhoneNumber = UserVm.PhoneNumber,
                    RoleId = UserVm.RoleId,
                    IsCreateOperation = false
                };
            }
            else
            {
                NavigationManager.NavigateTo("/users");
            }
        }
        catch (HttpRequestException ex)
        {
            ToastService.ShowError(ex.Message);
        }
    }

    private async Task HandleUserUpdate()
    {
        try
        {
            if (UserId.HasValue)
            {
                isLoadingButton = true;

                await UserService.UpdateUserAsync((Guid)UserId, User);
                isLoadingButton = false;

                NavigationManager.NavigateTo($"/users/detail/{UserId}");
            }
            else
            {
                isLoadingButton = false;
                ToastService.ShowError("Ooops! Terjadi kesalahan.");
            }
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