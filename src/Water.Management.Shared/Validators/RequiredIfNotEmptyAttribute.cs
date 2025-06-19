using System.ComponentModel.DataAnnotations;

namespace Water.Management.Shared.Validators;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple =false)]
public class RequiredIfNotEmptyAttribute : ValidationAttribute
{
    private readonly string _requiredIfOtherFilled;
    public RequiredIfNotEmptyAttribute(string requiredIfOtherFilled)
        : base($"Required when {requiredIfOtherFilled} is empty")
    {
        _requiredIfOtherFilled = requiredIfOtherFilled;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var requiredIfOtherFilled = validationContext.ObjectType.GetProperty(_requiredIfOtherFilled)?.GetValue(validationContext.ObjectInstance, null);
        if (string.IsNullOrWhiteSpace(requiredIfOtherFilled?.ToString()) && IsValueEmpty(value))
            return new ValidationResult(ErrorMessage, new[] {validationContext.MemberName!});

        return ValidationResult.Success;
    }

    private static bool IsValueEmpty(object? value)
    {
        return value switch
        {
            null => true,
            Array array => array.Length == 0,
            string str => string.IsNullOrWhiteSpace(str),
            _ => false,
        };
    }
}
