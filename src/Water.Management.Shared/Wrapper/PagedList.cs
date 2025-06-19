using System.Text.Json.Serialization;

namespace Shared.Wrapper;

public class PagedList<T> where T : class
{
    public PagedList()
    {
        Items = new List<T>();
        Page = 0;
        PageSize = 0;
        HasNextPage = false;
    }

    [JsonPropertyName("items")]
    public List<T> Items { get; set; }
    [JsonPropertyName("page")]
    public int Page { get; set; }
    [JsonPropertyName("pageSize")]
    public int PageSize { get; set; }
    [JsonPropertyName("hasNextPage")]
    public bool HasNextPage { get; set; }
}

