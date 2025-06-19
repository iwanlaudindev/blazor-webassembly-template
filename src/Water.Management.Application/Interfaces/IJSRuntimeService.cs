namespace Water.Management.Application.Interfaces;

public interface IJSRuntimeService
{
    Task<string> GetAsync(string tokenName);
    Task SetAsync(string tokenName, string tokenValue);
    Task RemoveAsync(string tokenName);
}
