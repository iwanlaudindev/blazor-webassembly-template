using Water.Management.Application.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace Water.Management.Client.Shared.Components;

public partial class Header
{
    [Inject] private IAuthService AuthService { get; set; } = default!;
    [Inject] private NavigationManager NavigationManager { get; set; } = default!;
    [Inject] private IJSRuntime JSRuntime { get; set; } = default!;
    
    [CascadingParameter]
    public Task<AuthenticationState> AuthState { get; set; } = null!;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            var module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./js/site.js");
            await module.InvokeVoidAsync("addToggleEvent");
        }
    }

    private async Task HandleSignOut()
    {
        await AuthService.SignOut();
        NavigationManager.NavigateTo("/auth/signin", true);
    }
}