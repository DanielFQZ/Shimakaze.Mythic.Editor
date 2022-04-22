using System.Globalization;

namespace Shimakaze.Mythic.Editor.ValueConverters;
[ValueConversion(typeof(decimal?), typeof(string))]
public sealed class DecimalStringValueConverter : IValueConverter
{
    public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value?.ToString() ?? null;
    }

    public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return decimal.TryParse(value?.ToString(), out decimal result) ? result : null;
    }
}
