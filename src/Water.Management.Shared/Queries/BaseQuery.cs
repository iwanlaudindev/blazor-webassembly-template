namespace Water.Management.Shared.Queries;

public class BaseQuery
{
    public BaseQuery()
    {
        Page = 1;
        PageSize = 5;
    }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public string? Search { get; set; }
}
