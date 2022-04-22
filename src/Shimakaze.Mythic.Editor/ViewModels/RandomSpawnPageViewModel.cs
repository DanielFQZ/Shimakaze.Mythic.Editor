using Shimakaze.Mythic.Editor.Models;
using Shimakaze.Mythic.Editor.Services;

namespace Shimakaze.Mythic.Editor.ViewModels;

/// <summary>
/// RandomSpawn Page 的视图模型
/// </summary>
public sealed class RandomSpawnPageViewModel : PageViewModelBase<RandomSpawnCardViewModel, RandomSpawn>
{
    protected override string NewItemName { get; } = "New_RandomSpawn";
}