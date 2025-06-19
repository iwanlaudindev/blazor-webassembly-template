using System.ComponentModel.DataAnnotations;

namespace Water.Management.Shared.Models;

public class Approval
{
    [Required]
    public bool Approved { get; set; }
    public string? Remark { get; set; }
}
