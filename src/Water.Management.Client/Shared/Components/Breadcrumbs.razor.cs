using Water.Management.Client.Models;
using Microsoft.AspNetCore.Components;

namespace Water.Management.Client.Shared.Components;

public partial class Breadcrumbs
{
    // Demonstrates how a parent component can supply parameters
    [Parameter]
    public string? Title { get; set; }

    [Parameter]
    public List<BreadcrumbItem> BreadcrumbItems { get; set; } = new List<BreadcrumbItem>();
}