using System.ComponentModel;

namespace tech_test_payment.domain.Shared;

public static class EnumHelper
{
    public static string GetEnumDescription(this Enum enumValue)
    {
        var field = enumValue.GetType().GetField(enumValue.ToString());
        if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
        {
            return attribute.Description;
        }
        throw new ArgumentException("Item not found.", nameof(enumValue));
    }

    public static bool EnumIsDefinedByType(Type enumType, object value)
    {
        return Enum.IsDefined(enumType, value);
    }
}
