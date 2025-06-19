using Blazored.LocalStorage;
using Water.Management.Application.Comman;
using Water.Management.Application.Constants;
using Water.Management.Application.Extensions;
using Water.Management.Application.Interfaces;
using Water.Management.Shared.Models;
using Water.Management.Shared.ViewModels;
using Microsoft.AspNetCore.Components.Authorization;
using Shared.Wrapper;
using System.Text.Json;

namespace Water.Management.Application.Services;

public class AuthService : IAuthService
{
    private readonly IHttpClientFactory _httpClient;
    private readonly AuthenticationStateProvider _authStateProvider;
    private readonly ILocalStorageService _localStorageService;

    public AuthService(
        IHttpClientFactory httpClient,
        AuthenticationStateProvider authStateProvider,
        ILocalStorageService localStorageService)
    {
        _httpClient = httpClient;
        _authStateProvider = authStateProvider;
        _localStorageService = localStorageService;
    }

    public async Task SignInAsync(Authentication request)
    {
        var response = await _httpClient.PostAsync($"{ApiEndpoint.SignIn}", request);
        var responseBody = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            Error err = JsonSerializer.Deserialize<Error>(responseBody)!;
            throw new Exception(err.Message);
        }

        var result = JsonSerializer.Deserialize<AuthenticationVm>(responseBody)!;
        await _localStorageService.SetItemAsStringAsync("AUTH_TOKEN", result!.AccessToken);
        await _localStorageService.SetItemAsStringAsync("REFRESH_TOKEN", result!.RefreshToken);

        ((CustomAuthenticationStateProvider)_authStateProvider).MarkUserAsAuthenticated(result!.AccessToken);

        response.EnsureSuccessStatusCode();
    }

    public async Task SignOut()
    {
        await ((CustomAuthenticationStateProvider)_authStateProvider)
            .MarkUserAsLoggedOutAsync();
    }
}

