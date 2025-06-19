using Water.Management.Shared.Models;

namespace Water.Management.Application.Interfaces;

public interface IAuthService
{
    Task SignInAsync(Authentication request);
    Task SignOut();
}

