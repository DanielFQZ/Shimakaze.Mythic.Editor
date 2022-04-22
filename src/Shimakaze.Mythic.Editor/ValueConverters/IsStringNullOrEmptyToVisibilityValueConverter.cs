using System.Globalization;

namespace Shimakaze.Mythic.Editor.ValueConverters;

/// <summary>
/// 这是判断字符串是否为空并返回对应Visibility的转换器
/// </summary>
[ValueConversion(typeof(string), typeof(Visibility))]
public sealed class IsStringNullOrEmptyToVisibilityValueConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return string.IsNullOrEmpty(value?.ToString()) ? Visibility.Collapsed : Visibility.Visible;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
