using Shared.Wrapper;
using System.Net.Http.Json;
using System.Text.Json;

namespace Water.Management.Application.Extensions;

public static class HttpResponseMessageExtensions
{
    public static async Task HandleErrorResponseAsync(this HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            Error error = await response.Content.ReadFromJsonAsync<Error>() 
                ?? throw new JsonException("Failed to deserialize error response.");
            throw new HttpRequestException(error.Message);
        }
    }
}
