using Water.Management.Shared.ViewModels;

namespace Water.Management.Application.Interfaces;

public interface IDashboardService
{
    Task<DashboardSummaryVm> GetSummary();

    Task<List<WaterSourceVm>> GetWaterSupplyPendingStatus();
}
