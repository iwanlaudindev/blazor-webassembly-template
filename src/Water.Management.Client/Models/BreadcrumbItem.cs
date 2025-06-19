namespace Water.Management.Client.Models;

public class BreadcrumbItem
{
    public BreadcrumbItem(string label, string? href, bool isActive)
    {
        Label = label;
        Href = href;
        IsActive = isActive;
    }

    public string? Label { get; set; }
    public string? Href { get; set; }
    public bool IsActive { get; set; }
}

