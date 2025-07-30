using System.ComponentModel.DataAnnotations;

namespace Rika_OrderProvider.Infrastructure.Helpers;

public class CustomValidation
{
    public static ValidationModel<T> ValidateModel<T>(T model)
    {
        if (model == null)
        {
            return new ValidationModel<T>
            {
                IsValid = false,
                ValidationResults = new List<ValidationResult>
                {
                    new ValidationResult("Request cannot be null.")
                }
            };
        }

        var results = new List<ValidationResult>();
        var isValid = TryValidateObjectRecursive(model, results);

        return new ValidationModel<T>
        {
            IsValid = isValid,
            Value = model,
            ValidationResults = results
        };
    }

    private static bool TryValidateObjectRecursive(object model, List<ValidationResult> results)
    {
        var context = new ValidationContext(model, serviceProvider: null, items: null);
        bool isValid = Validator.TryValidateObject(model, context, results, true);

        var properties = model.GetType().GetProperties()
            .Where(prop => prop.CanRead && prop.GetIndexParameters().Length == 0 && prop.PropertyType != typeof(string))
            .ToList();

        foreach (var property in properties)
        {
            var value = property.GetValue(model);
            if (value != null)
            {
                if (typeof(IEnumerable<object>).IsAssignableFrom(property.PropertyType))
                {
                    var collection = (IEnumerable<object>)value;
                    foreach (var item in collection)
                    {
                        isValid &= TryValidateObjectRecursive(item, results);
                    }
                }
                else
                {
                    isValid &= TryValidateObjectRecursive(value, results);
                }
            }
        }

        return isValid;
    }

    public class ValidationModel<T>
    {
        public bool IsValid { get; set; }
        public T? Value { get; set; }
        public List<ValidationResult> ValidationResults { get; set; } = new List<ValidationResult>();
    }
}