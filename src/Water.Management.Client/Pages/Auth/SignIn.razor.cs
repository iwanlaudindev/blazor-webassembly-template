using Blazored.Toast.Services;
using Water.Management.Application.Interfaces;
using Water.Management.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace Water.Management.Client.Pages.Auth;

public partial class SignIn
{
    [Inject] private NavigationManager NavigationManager { get; set; } = default!;
    [Inject] private IToastService ToastService { get; set; } = default!;
    [Inject] private IAuthService AuthService { get; set; } = default!;

    public Authentication Authentication { get; set; } = new();
    private bool isLoadingButton = false;

    public async Task HandleUserSignin()
    {
        try
        {
            isLoadingButton = true;

            await AuthService.SignInAsync(Authentication);
            isLoadingButton = false;

            NavigationManager.NavigateTo("/");
        }
        catch (Exception ex)
        {
            isLoadingButton = false;
            ToastService.ShowError(ex.Message);
        }
    }
}