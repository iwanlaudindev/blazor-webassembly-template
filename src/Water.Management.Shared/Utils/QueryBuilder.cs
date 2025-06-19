using System.Text;
using Water.Management.Shared.Queries;

namespace Water.Management.Shared.Utils;

public static class QueryBuilder
{
    public static StringBuilder GetStringBuilder(this BaseQuery query)
    {
        StringBuilder queries = new();
        if (!string.IsNullOrWhiteSpace(query.Search))
        {
            if (queries.Length > 0)
                queries.Append($"&search={query.Search}");
            else
                queries.Append($"?search={query.Search}");
        }

        if (query.Page > 1)
        {
            if (queries.Length > 0)
                queries.Append($"&page={query.Page}");
            else
                queries.Append($"?page={query.Page}");
        }

        if (query.PageSize > 5)
        {
            if (queries.Length > 0)
                queries.Append($"&size={query.PageSize}");
            else
                queries.Append($"?size={query.PageSize}");
        }

        return queries;
    }
}
