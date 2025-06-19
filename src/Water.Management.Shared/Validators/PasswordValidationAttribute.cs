using Water.Management.Shared.Models;
using System.ComponentModel.DataAnnotations;

namespace Water.Management.Shared.Validators;

public class PasswordValidationAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var user = (User)validationContext.ObjectInstance;
        if (user != null)
        {
            if (user.IsCreateOperation && string.IsNullOrWhiteSpace(user.Password))
            {
                return new ValidationResult("Password is required.", new[] { nameof(User.Password) });
            }
        }
        return ValidationResult.Success;
    }
}

