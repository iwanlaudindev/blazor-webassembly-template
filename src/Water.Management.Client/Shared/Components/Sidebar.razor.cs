using Water.Management.Application.Interfaces;
using Microsoft.AspNetCore.Components;

namespace Water.Management.Client.Shared.Components;

public partial class Sidebar
{
    [Inject] private IJSRuntimeService JSRuntimeService { get; set; } = default!;

    private string activeNavLink = "";
    private string activeCollapse = "";

    protected override async Task OnInitializedAsync()
    {
        var storedActiveNavLink = await JSRuntimeService.GetAsync("activeNavLink");
        if (!string.IsNullOrEmpty(storedActiveNavLink))
            activeNavLink = storedActiveNavLink;
        else
            activeNavLink = "dashboard";
    }

    private string GetNavLinkClass(string navLinkId)
    {
        return navLinkId == activeNavLink ? "nav-link" : "nav-link collapsed";
    }

    private string GetCollapseClass(string collapseId)
    {
        return activeCollapse == collapseId ? "nav-content collapse show" : "nav-content collapse";
    }

    private async Task ToggleCollapse(string id)
    {
        if (id != activeCollapse)
        {
            activeCollapse = id;
            activeNavLink = id;
        }

        await JSRuntimeService.SetAsync("activeNavLink", activeNavLink);
    }
}