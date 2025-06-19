using Shared.Wrapper;
using Water.Management.Shared.Models;
using Water.Management.Shared.Queries;
using Water.Management.Shared.ViewModels;

namespace Water.Management.Application.Interfaces;

public interface IWaterSourceService
{
    Task<PagedList<WaterSourceVm>> GetAllAsync(WaterSourceQuery query);
    Task<WaterSourceVm> GetByIdAsync(Guid waterSupplyId);
    Task CreateAsync(MultipartFormDataContent dataContent);
    Task UpdateAsync(Guid waterSupplyId, MultipartFormDataContent dataContent);
    Task SetApprovalAsync(Guid waterSupplyId, Approval approval);
    Task<List<WaterTypeVm>> GetAllWaterTypeAsync();
    Task<List<WaterConditionVm>> GetAllWaterConditionAsync();
    Task<List<WaterSourceVm>> GetAllReportAsync(DateOnly? from, DateOnly? to);
}
