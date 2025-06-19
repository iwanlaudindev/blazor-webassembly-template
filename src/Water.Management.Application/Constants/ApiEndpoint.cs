namespace Water.Management.Application.Constants;

public class ApiEndpoint
{
    /// <summary>
    /// Identity Endpoint API
    /// </summary>
    public const string SignIn = "/api/identity/sign-in";
    public const string Refresh = "/api/identity/refresh";

    /// <summary>
    /// Dashboard Endpoint API
    /// </summary>
    public const string Summary = "/api/dashboard/summary";
    public const string GetWaterSourcePendingStatus = "/api/dashboard/water-supplies";

    /// <summary>
    /// Region Endpoint API
    /// </summary>
    public const string GetAllProvince = "/api/region/provinces";
    public const string GetAllRegency = "/api/region/cities";
    public const string GetAllDistrict = "/api/region/districts";
    public const string GetAllVillage = "/api/region/sub-districts";

    /// <summary>
    /// User Endpoint API
    /// </summary>
    public const string GetAllUsers = "/api/user-management/users";
    public const string GetUserById = "/api/user-management/users/";
    public const string GetAllUserRoles = "/api/user-management/users/roles";
    public const string CreateUser = "/api/user-management/users";
    public const string UpdateUser = "/api/user-management/users/";
    public const string ResetPassword = "/api/user-management/users/{userId}/reset-password";

    /// <summary>
    /// Water Supply Endpoint API
    /// </summary>
    public const string GetAllWaterSupply = "/api/water-supply-management/water-supplies";
    public const string GetWaterSupplyById = "/api/water-supply-management/water-supplies/";
    public const string CreateWaterSupply = "/api/water-supply-management/water-supplies"; 
    public const string UpdateWaterSupply = "/api/water-supply-management/water-supplies/";
    public const string SetApprovalStatus = "/api/water-supply-management/water-supplies/";
    public const string GetAllWaterSupplyType = "/api/water-supply-management/water-supplies/types"; 
    public const string GetAllWaterSupplyCondition = "/api/water-supply-management/water-supplies/conditions"; 
    public const string GetAllWaterSupplyReport = "/api/water-supply-management/water-supplies/report";

    /// <summary>
    /// Geophysics Endpoint API
    /// </summary>
    public const string GetAllGeophysics = "/api/geophysics-management/geophysics";
    public const string GetByIdGeophysics = "/api/geophysics-management/geophysics/";
    public const string CreateGeophysics = "/api/geophysics-management/geophysics";
    public const string UpdateGeophysics = "/api/geophysics-management/geophysics/";
    public const string SetApprovalStatusGeophysics = "/api/geophysics-management/geophysics/";
    public const string GetAllGeophysicsReport = "/api/geophysics-management/geophysics/report";

    /// <summary>
    /// GIS API Endpoint
    /// </summary>
    public const string GetAllWaterSupplyCoordinate = "/api/gis/water-supplies";
    public const string GetAllGeophysicsCoordinate = "/api/gis/geophysics";
    public const string GetDetailWaterSupply = "/api/gis/water-supplies/";
    public const string GetDetailGeophysics = "/api/gis/geophysics/";
}

