using Blazored.LocalStorage;
using Water.Management.Application.Constants;
using Water.Management.Application.Extensions;
using Water.Management.Shared.Models;
using Water.Management.Shared.ViewModels;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;

namespace Water.Management.Application.Comman;

public class CustomHttpClientHandler : DelegatingHandler
{
    private readonly ILocalStorageService _localStorageService;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly AuthenticationStateProvider _authStateProvider;
    private readonly NavigationManager _navigationManager;

    public CustomHttpClientHandler(
        ILocalStorageService localStorageService,
        IHttpClientFactory httpClientFactory,
        AuthenticationStateProvider authStateProvider,
        NavigationManager navigationManager)
    {
        _localStorageService = localStorageService;
        _httpClientFactory = httpClientFactory;
        _authStateProvider = authStateProvider;
        _navigationManager = navigationManager;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, 
        CancellationToken cancellationToken)
    {
        var excludedPaths = new string[] { "sign-in", "refresh", "gis" };
        if (excludedPaths.Any(path => request.RequestUri!.AbsolutePath.ToLower().Contains(path)))
            return await base.SendAsync(request, cancellationToken);
        
        var token = await _localStorageService.GetItemAsStringAsync("AUTH_TOKEN", cancellationToken);
        if (!string.IsNullOrWhiteSpace(token))
            request.Headers.Add("Authorization", $"Bearer {token}");

        var response =  await base.SendAsync(request, cancellationToken);
        if (response.StatusCode == HttpStatusCode.Unauthorized)
            return await InvokeRefreshTokenCall(request, cancellationToken);

        return response;
    }

    private async Task<HttpResponseMessage> InvokeRefreshTokenCall(
        HttpRequestMessage request, 
        CancellationToken cancellationToken)
    {
        var refreshToken = await _localStorageService.GetItemAsStringAsync("REFRESH_TOKEN", cancellationToken);
        if (string.IsNullOrWhiteSpace(refreshToken))
        {
            await ((CustomAuthenticationStateProvider)_authStateProvider).MarkUserAsLoggedOutAsync();
            _navigationManager.NavigateTo("/auth/signin", true);
            throw new UnauthorizedAccessException("Not authorized.");
        }
            
        var newRequest = new AuthRefreshToken(refreshToken);
        var refrehTokenResponse = await _httpClientFactory.PostAsync(ApiEndpoint.Refresh, newRequest);
        if (refrehTokenResponse.StatusCode != HttpStatusCode.OK)
        {
            await ((CustomAuthenticationStateProvider)_authStateProvider).MarkUserAsLoggedOutAsync();
            _navigationManager.NavigateTo("/auth/signin", true);
            throw new UnauthorizedAccessException("Not authorized.");
        }

        var newToken = await refrehTokenResponse.Content.ReadFromJsonAsync<AuthenticationVm>(cancellationToken: cancellationToken);
        await _localStorageService.SetItemAsStringAsync("AUTH_TOKEN", newToken!.AccessToken, cancellationToken);
        await _localStorageService.SetItemAsStringAsync("REFRESH_TOKEN", newToken!.RefreshToken, cancellationToken);

        ((CustomAuthenticationStateProvider)_authStateProvider)
            .MarkUserAsAuthenticated(newToken!.AccessToken);

        // Update request with the new token
        request.Headers.Remove("Authorization");
        request.Headers.Add("Authorization", $"Bearer {newToken.AccessToken}");

        // Send the updated request
        return await base.SendAsync(request, cancellationToken);
    }
}

