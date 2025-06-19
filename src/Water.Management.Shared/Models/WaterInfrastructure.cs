using System.ComponentModel.DataAnnotations;

namespace Water.Management.Shared.Models;

public class WaterInfrastructure
{
    [Required]
    public Guid WaterInfrastructureId { get; set; }
}
