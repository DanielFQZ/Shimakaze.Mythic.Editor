namespace Shimakaze.Mythic.Editor.Extensions;
/// <summary>
/// 对.Net系统进行扩展
/// </summary>
internal static class SystemExtension
{
    /// <summary>
    /// 使用传入的对象做某些事情并返回这个对象<br/>
    /// 函数式编程小工具
    /// </summary>
    /// <typeparam name="T">对象的类型</typeparam>
    /// <param name="t">传入的对象</param>
    /// <param name="action">要做的什么事</param>
    /// <returns>传入的对象</returns>
    public static T Do<T>(this T t, Action<T> action)
    {
        action(t);
        return t;
    }
}
