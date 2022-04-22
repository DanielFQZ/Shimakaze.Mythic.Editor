using YamlDotNet.Serialization;

namespace Shimakaze.Mythic.Editor.Services;

/// <summary>
/// 模型读写操作
/// </summary>
public static class FileService
{
    /// <summary>
    /// 读取文件到模型
    /// </summary>
    /// <param name="reader">读取器</param>
    /// <returns>模型对象</returns>
    public static Dictionary<string, TModel> ReadFrom<TModel>(TextReader reader)
        where TModel : class
    {
        // 创建反序列化器
        var deserializer = new DeserializerBuilder()
            .Build();

        // 反序列化到对象
        return deserializer.Deserialize<Dictionary<string, TModel>>(reader);
    }

    /// <summary>
    /// 写入模型到文件
    /// </summary>
    /// <param name="data">模型</param>
    /// <param name="writer">写入器</param>
    public static void WriteTo<TModel>(Dictionary<string, TModel> data, TextWriter writer)
        where TModel : class
    {
        // 创建序列化器
        var serializer = new SerializerBuilder()
            // 配置忽略值为null的属性和空的集合
            .ConfigureDefaultValuesHandling(DefaultValuesHandling.OmitNull | DefaultValuesHandling.OmitEmptyCollections)
            .Build();

        // 序列化对象到流
        serializer.Serialize(writer, data, typeof(Dictionary<string, TModel>));
        writer.Flush();
    }
}