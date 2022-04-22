using System.Runtime.Versioning;

namespace Shimakaze.Mythic.Editor.ViewModels;

public interface INameViewModel<TModel>
{
    string Name { get; set; }

    internal TModel ToModel();
    [RequiresPreviewFeatures]
    internal static abstract INameViewModel<TModel> FromModel(string name, TModel model);
    [RequiresPreviewFeatures]
    internal static abstract INameViewModel<TModel> CreateEmpty(string name);
}
public interface ISubViewModel<TModel>
{
    internal TModel ToModel();
}
