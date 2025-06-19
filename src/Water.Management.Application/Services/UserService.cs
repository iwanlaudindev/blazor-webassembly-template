using Water.Management.Application.Constants;
using Water.Management.Application.Extensions;
using Water.Management.Application.Interfaces;
using Water.Management.Shared.Models;
using Water.Management.Shared.ViewModels;
using Shared.Wrapper;
using System.Net.Http.Json;
using System.Text;
using Water.Management.Shared.Utils;
using Water.Management.Shared.Queries;

namespace Water.Management.Application.Services;

public class UserService : IUserService
{
    private readonly IHttpClientFactory _httpClient;

    public UserService(IHttpClientFactory httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<PagedList<UserVm>> GetAllUserAsync(BaseQuery query)
    {
        StringBuilder queries = query.GetStringBuilder();

        HttpResponseMessage response = await _httpClient.GetAsync($"{ApiEndpoint.GetAllUsers}{queries}");
        await response.HandleErrorResponseAsync();

        var result = await response.Content.ReadFromJsonAsync<PagedList<UserVm>>();
        return await Task.FromResult(result!);
    }

    public async Task<UserVm> GetUserByIdAsync(Guid userId)
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"{ApiEndpoint.GetUserById}{userId}");
        await response.HandleErrorResponseAsync();

        UserVm? result = await response.Content.ReadFromJsonAsync<UserVm>();
        return await Task.FromResult(result!);
    }

    public async Task<List<RoleVm>> GetAllUserRolesAsync()
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"{ApiEndpoint.GetAllUserRoles}");
        await response.HandleErrorResponseAsync();

        var result = await response.Content.ReadFromJsonAsync<List<RoleVm>>();
        return await Task.FromResult(result!);
    }

    public async Task CreateUserAsync(User user)
    {
        HttpResponseMessage response = await _httpClient.PostAsync($"{ApiEndpoint.CreateUser}", user);
        await response.HandleErrorResponseAsync();
    }

    public async Task UpdateUserAsync(Guid userId, User user)
    {
        HttpResponseMessage response = await _httpClient.PutAsync($"{ApiEndpoint.UpdateUser}{userId}", user);
        await response.HandleErrorResponseAsync();
    }

    public async Task ResetPasswordAsync(Guid userId)
    {
        HttpResponseMessage response = await _httpClient.PatchAsync($"{ApiEndpoint.ResetPassword.Replace("{userId}", userId.ToString())}");
        await response.HandleErrorResponseAsync();
    }
}

