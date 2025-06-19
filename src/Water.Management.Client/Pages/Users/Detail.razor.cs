using Blazored.Toast.Services;
using Water.Management.Application.Interfaces;
using Water.Management.Shared.ViewModels;
using Microsoft.AspNetCore.Components;
using Water.Management.Shared.Models;

namespace Water.Management.Client.Pages.Users;

public partial class Detail
{
    [Inject] private NavigationManager NavigationManager { get; set; } = default!;
    [Inject] private IUserService UserService { get; set; } = default!;
    [Inject] private IToastService ToastService { get; set; } = default!;

    [Parameter] public Guid? UserId { get; set; }

    private bool isLoading = true;
    private bool isApproved = false;
    private string? message;
    private string? title;
    public UserVm? User { get; set; }

    protected override async Task OnInitializedAsync()
    {
        if (UserId.HasValue)
        {
            try
            {
                User = await UserService.GetUserByIdAsync((Guid)UserId);
                isLoading = false;
            }
            catch (HttpRequestException ex)
            {
                ToastService.ShowError(ex.Message);
            }
        }
        else
        {
            NavigationManager.NavigateTo("/users");
        }
    }

    private async Task HandleResetPassword(Guid userId)
    {
        try
        {
            await UserService.ResetPasswordAsync(userId);
            isApproved = true;
            title = "Password reset successfully!";
            message = $"The user password {User?.FullName} was successfully reset to the default password: 123456";
        }
        catch(Exception ex)
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
}