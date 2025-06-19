using Water.Management.Shared.Validators;
using System.ComponentModel.DataAnnotations;

namespace Water.Management.Shared.Models;

public class User
{
    [Required]
    [RegularExpression(@"^\S*$", ErrorMessage = "Username cannot contain spaces.")]
    public string? Username { get; set; }
    [Required]
    [Display(Name ="Full Name")]
    public string? FullName { get; set; }
    [Required]
    public string? Email { get; set; }
    [Required]
    [RegularExpression(@"^[0-9]{10,12}$", ErrorMessage = "Invalid phone number.")]
    [Display(Name ="Phone Number")]
    public string? PhoneNumber { get; set; }
    [Required]
    [Display(Name ="Role")]
    public string? RoleId { get; set; }
    [PasswordValidation]
    public string? Password { get; set; }
    public bool IsCreateOperation { get; set; } = true;
}

