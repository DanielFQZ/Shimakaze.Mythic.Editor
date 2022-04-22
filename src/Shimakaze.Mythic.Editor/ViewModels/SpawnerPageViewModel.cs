using Shimakaze.Mythic.Editor.Models;

namespace Shimakaze.Mythic.Editor.ViewModels;

/// <summary>
/// Spawner Page ����ͼģ��
/// </summary>
public sealed class SpawnerPageViewModel : PageViewModelBase<SpawnerCardViewModel, Spawner>
{
    protected override string NewItemName { get; } = "New_Spawner";
}
