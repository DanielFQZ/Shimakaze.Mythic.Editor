using Shimakaze.Mythic.Editor.I18n;

namespace Shimakaze.Mythic.Editor.Extensions;

/// <summary>
/// UI的本地化实现工具
/// </summary>
internal static class I18nExtension
{
    /// <summary>
    /// 对整个UI控件及其子控件进行本地化
    /// </summary>
    /// <param name="ctrl">控件</param>
    public static void UidI18nInitialize(this UIElement ctrl)
    {
        new HashSet<UIElement> { ctrl }
            .Do(ctrl.FindLogicChild)
            .Do(ctrl.FindVisualChild)
            .Where(i => !string.IsNullOrEmpty(i.Uid))
            .Each(ApplyResourceProperty);
    }

    /// <summary>
    /// [反射] 应用本地化
    /// </summary>
    /// <param name="element">控件</param>
    private static void ApplyResourceProperty(this UIElement element)
    {
        element
            .GetType() // 获取类型
            .GetProperties() // 获取所有public的属性
            .Where(p => (p.PropertyType == typeof(string) || p.PropertyType == typeof(object)) && p.CanWrite) // 过滤掉非字符串类型的属性和不可写的属性
            .Each(prop =>
        {
            if (Resource.ResourceManager.GetString($"{element.Uid}.{prop.Name}") is string value) // 查找存在对应属性的资源
            {
                prop.SetValue(element, value); // 反射应用本地化字符串
            }
        });
    }

    /// <summary>
    /// 查找子控件
    /// </summary>
    /// <typeparam name="T">控件类型</typeparam>
    /// <param name="parent">父控件依赖对象</param>
    /// <param name="list">子控件列表</param>
    private static void FindLogicChild<T>(this DependencyObject parent, ICollection<T> list) where T : DependencyObject
    {
        LogicalTreeHelper
            .GetChildren(parent ?? throw new ArgumentNullException(nameof(parent)))
            .OfType<DependencyObject>()
            .Each(v =>
        {
            if (v is T t)
                list.Add(t);
            v.FindLogicChild(list);
        });
    }

    /// <summary>
    /// 查找子控件
    /// </summary>
    /// <typeparam name="T">控件类型</typeparam>
    /// <param name="parent">父控件依赖对象</param>
    /// <param name="list">子控件列表</param>
    private static void FindVisualChild<T>(this DependencyObject parent, ICollection<T> list) where T : DependencyObject
    {
        int count = VisualTreeHelper.GetChildrenCount(parent ?? throw new ArgumentNullException(nameof(parent)));
        for (int i = 0; i < count; i++)
        {
            var v = VisualTreeHelper.GetChild(parent, i);
            if (v is T t)
                list.Add(t);
            v.FindVisualChild(list);
        }
    }
}
