namespace Water.Management.Shared.Queries;

public class CoordinateTypeQuery
{
    public bool IsNotEmptyWaterSource { get; set; }
    public bool IsNotEmptyGeophysics { get; set; }

    public void SetIsNotEmptyWaterSource()
    {
        IsNotEmptyWaterSource = !IsNotEmptyWaterSource;
    }

    public void SetIsNotEmptyGeophysics()
    {
        IsNotEmptyGeophysics = !IsNotEmptyGeophysics;
    }
}
