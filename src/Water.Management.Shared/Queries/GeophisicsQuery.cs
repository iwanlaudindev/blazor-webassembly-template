namespace Water.Management.Shared.Queries;

public class GeophisicsQuery : BaseQuery
{
    public string? Status { get; set; }
    public string? ProvinceId { get; set; }
    public string? CityId { get; set; }
    public string? DistrictId { get; set; }
    public string? SubDistrictId { get; set; }
    public DateOnly? From { get; set; }
    public DateOnly? To { get; set; }
}
