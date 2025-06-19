using System.ComponentModel.DataAnnotations;

namespace Water.Management.Shared.Models;

public class WaterCondition
{
    [Required]
    public Guid WaterConditionId { get; set; }
}
