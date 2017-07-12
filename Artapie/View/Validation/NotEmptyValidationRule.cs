namespace View.Validation
{
    using System.Globalization;
    using System.Windows.Controls;

    internal class NotEmptyValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            return string.IsNullOrWhiteSpace((value ?? "").ToString())
                       ? new ValidationResult(false, "Obligatoire")
                       : ValidationResult.ValidResult;
        }
    }
}
