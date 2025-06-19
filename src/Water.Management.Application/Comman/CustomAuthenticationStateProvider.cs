using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Text.Json;

namespace Water.Management.Application.Comman;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorageService;

    public CustomAuthenticationStateProvider(ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var jwtToken = await _localStorageService.GetItemAsStringAsync("AUTH_TOKEN");
        if (string.IsNullOrEmpty(jwtToken) || jwtToken.Split(".").Length < 3)
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(jwtToken), "jwt"));

        return new AuthenticationState(authenticatedUser);
    }

    public void MarkUserAsAuthenticated(string token)
    {
        var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt"));
        var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
        NotifyAuthenticationStateChanged(authState);
    }

    public async Task MarkUserAsLoggedOutAsync()
    {
        await _localStorageService.ClearAsync();
        var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
        var authState = Task.FromResult(new AuthenticationState(anonymousUser));
        NotifyAuthenticationStateChanged(authState);
    }

    private static IEnumerable<Claim> ParseClaimsFromJwt(string token)
    {
        var parts = token.Split(".");
        var payloadBytes = ParseBase64WithoutPadding(parts[1]);

        var decodedPayload = JsonSerializer.Deserialize<Dictionary<string, object>>(payloadBytes);
        var claims = decodedPayload!.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()!));

        return claims;
    }

    private static byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }
        return Convert.FromBase64String(base64);
    }
}
