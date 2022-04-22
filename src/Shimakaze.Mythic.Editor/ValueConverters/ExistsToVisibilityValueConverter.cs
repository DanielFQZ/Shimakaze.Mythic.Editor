using System.Globalization;

namespace Shimakaze.Mythic.Editor.ValueConverters;

[ValueConversion(typeof(object), typeof(Visibility))]
public sealed class ExistsToVisibilityValueConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value is null ? Visibility.Collapsed : Visibility.Visible;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
