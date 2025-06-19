using Water.Management.Shared.ViewModels;

namespace Water.Management.Application.Interfaces;

public interface IGisService
{
    Task<List<CoordinateVm>> GetWaterCoordinateAsync();
    Task<List<CoordinateVm>> GetGeophysicsCoorinateAsync();
    Task<WaterSourceVm> GetDetailWaterSupplyAsync(Guid waterSupplyId);
}
