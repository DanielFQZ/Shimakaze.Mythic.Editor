using System.Globalization;

namespace Shimakaze.Mythic.Editor.ValueConverters;

[ValueConversion(typeof(string), typeof(decimal?))]
public sealed class StringAndNullableDecimalValueConverter : IValueConverter
{
    public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return decimal.TryParse(value?.ToString(), out decimal result) ? result : null;
    }

    public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value?.ToString() ?? null;
    }
}
