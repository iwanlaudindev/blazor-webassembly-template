using System.Text;
using System.Text.Json;


namespace Water.Management.Application.Extensions;

public static class HttpClientExtensions
{
    public static async Task<HttpResponseMessage> GetAsync(this IHttpClientFactory client, string url)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        return await client.CreateClient("komodoWaterApiV1").SendAsync(request);
    }

    public static async Task<HttpResponseMessage> PostAsync(this IHttpClientFactory client, string url, object data)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, url)
        {
            Content = new StringContent(JsonSerializer.Serialize(data),
            Encoding.UTF8,
            "application/json")
        };

        return await client.CreateClient("komodoWaterApiV1").SendAsync(request);
    }

    public static async Task<HttpResponseMessage> PostAsFormAsync(this IHttpClientFactory client, string url, MultipartFormDataContent formData)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, url)
        {
            Content = formData
        };

        return await client.CreateClient("komodoWaterApiV1").SendAsync(request);
    }

    public static async Task<HttpResponseMessage> PutAsync(this IHttpClientFactory client, string url, object data)
    {
        var request = new HttpRequestMessage(HttpMethod.Put, url)
        {
            Content = new StringContent(JsonSerializer.Serialize(data),
            Encoding.UTF8,
            "application/json")
        };

        return await client.CreateClient("komodoWaterApiV1").SendAsync(request);
    }

    public static async Task<HttpResponseMessage> PutAsFormAsync(this IHttpClientFactory client, string url, MultipartFormDataContent formData)
    {
        var request = new HttpRequestMessage(HttpMethod.Put, url)
        {
            Content = formData
        };

        return await client.CreateClient("komodoWaterApiV1").SendAsync(request);
    }

    public static async Task<HttpResponseMessage> PatchAsync(this IHttpClientFactory client, string url, object? data = null)
    {
        var request = new HttpRequestMessage(HttpMethod.Patch, url)
        {
            Content = new StringContent(JsonSerializer.Serialize(data),
            Encoding.UTF8,
            "application/json")
        };

        return await client.CreateClient("komodoWaterApiV1").SendAsync(request);
    }

    public static async Task<HttpResponseMessage> DeleteAsync(this IHttpClientFactory client, string url)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, url);

        return await client.CreateClient("komodoWaterApiV1").SendAsync(request);
    }
}
