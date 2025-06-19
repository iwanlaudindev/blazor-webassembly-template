using Water.Management.Shared.ViewModels;

namespace Water.Management.Application.Interfaces;

public interface IRegionService
{
    Task<List<ProvinceVm>> GetAllProvinceAsync();
    Task<List<CityVm>> GetAllCityAsync(string provinceId);
    Task<List<DistrictVm>> GetAllDistrictAsync(string cityId);
    Task<List<SubDistrictVm>> GetAllSubDistrictAsync(string districtId);
}
