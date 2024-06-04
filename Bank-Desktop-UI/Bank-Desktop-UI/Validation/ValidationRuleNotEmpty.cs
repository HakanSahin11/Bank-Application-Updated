using System.Globalization;
using System.Windows.Controls;

namespace Bank_Desktop_UI.Validation
{
    public class ValidationRuleNotEmpty : ValidationRule
    {       
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return new ValidationResult(false, "Field cannot be empty.");
            }
            return ValidationResult.ValidResult;
        }
    }
}
