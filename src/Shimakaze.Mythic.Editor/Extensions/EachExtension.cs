namespace Shimakaze.Mythic.Editor.Extensions;
/// <summary>
/// 包含Each扩展方法的静态类
/// </summary>
internal static class EachExtension
{
    /// <summary>
    /// 将ForEach封装成一个扩展方法, 使用起来更方便
    /// </summary>
    /// <typeparam name="T">成员类型</typeparam>
    /// <param name="list">集合</param>
    /// <param name="action">要做的什么事</param>
    public static void Each<T>(this IEnumerable<T> list, Action<T> action)
    {
        foreach (var item in list)
            action(item);
    }
}