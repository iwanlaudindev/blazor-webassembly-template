using Blazored.Toast.Services;
using Water.Management.Application.Interfaces;
using Water.Management.Shared.ViewModels;
using Microsoft.AspNetCore.Components;
using Shared.Wrapper;
using Water.Management.Shared.Queries;

namespace Water.Management.Client.Pages.Users;

public partial class Index
{
    [Inject] IToastService ToastService { get; set; } = default!;
    [Inject] IUserService UserService { get; set; } = default!;

    private BaseQuery Query { get; set; } = new();
    private bool isLoading = true;

    public PagedList<UserVm>? users;

    protected override async Task OnInitializedAsync()
    {
        await LoadData();
        isLoading = false;
    }

    private async Task OnPageChange(int newPage)
    {
        Query.Page = newPage;
        await LoadData();

        if (!(users is not null && users.Items!.Count > 0))
        {
            ToastService.ShowWarning("Data is empty on this page.");

            Query.Page -= 1;
            await LoadData();
        }

        StateHasChanged();
    }

    private async Task LoadData()
    {
        try
        {
            users = await UserService.GetAllUserAsync(Query);
        }
        catch (Exception ex)
        {
            ToastService.ShowError(ex.Message);
        }
    }

    private async Task OnSearch(string s)
    {
        Query.Search = s;
        Query.Page = 1;
        await LoadData();
    }
}