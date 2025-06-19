using Water.Management.Shared.Models;
using Water.Management.Shared.ViewModels;
using Shared.Wrapper;
using Water.Management.Shared.Queries;

namespace Water.Management.Application.Interfaces;

public interface IUserService
{
    Task<PagedList<UserVm>> GetAllUserAsync(BaseQuery query);
    Task<UserVm> GetUserByIdAsync(Guid userId);
    Task CreateUserAsync(User user);
    Task UpdateUserAsync(Guid userId, User user);
    Task<List<RoleVm>> GetAllUserRolesAsync();
    Task ResetPasswordAsync(Guid userId);
}

