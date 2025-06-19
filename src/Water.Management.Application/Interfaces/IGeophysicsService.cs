using Shared.Wrapper;
using Water.Management.Shared.Models;
using Water.Management.Shared.Queries;
using Water.Management.Shared.ViewModels;

namespace Water.Management.Application.Interfaces;

public interface IGeophysicsService
{
    Task CreateAsync(MultipartFormDataContent dataContent);
    Task UpdateAsync(Guid geophysicsaId, MultipartFormDataContent dataContent);
    Task<PagedList<GeophysicsVm>> GetAllAsync(GeophisicsQuery query);
    Task<GeophysicsVm> GetByIdAsync(Guid geophysicsaId);
    Task SetApprovalAsync(Guid geophysicsaId, Approval approval);
    Task<List<GeophysicsVm>> GetAllReportAsync(DateOnly? from, DateOnly? to);
}
