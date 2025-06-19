using Water.Management.Application.Interfaces;
using Microsoft.JSInterop;

namespace Water.Management.Application.Services;

public class JSRuntimeService : IJSRuntimeService
{
    private readonly IJSRuntime _jsRuntime;

    public JSRuntimeService(IJSRuntime jSRuntime)
    {
        _jsRuntime = jSRuntime;
    }

    public async Task<string> GetAsync(string tokenName)
        => await _jsRuntime.InvokeAsync<string>("localStorage.getItem", tokenName);

    public async Task SetAsync(string tokenName, string tokenValue)
        => await _jsRuntime.InvokeVoidAsync("localStorage.setItem", tokenName, tokenValue);
    public async Task RemoveAsync(string tokenName)
        => await _jsRuntime.InvokeAsync<string>("localStorage.removeItem", tokenName);
}
