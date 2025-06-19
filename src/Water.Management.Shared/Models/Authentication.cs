using System.ComponentModel.DataAnnotations;

namespace Water.Management.Shared.Models;

public class Authentication
{
    [Required]
    public string? Username { get; set; }
    [Required]
    public string? Password { get; set; }
    public string? UserType { get; set; } = "a";
}

