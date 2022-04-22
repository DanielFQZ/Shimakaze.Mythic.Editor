using Shimakaze.Mythic.Editor.Models;

namespace Shimakaze.Mythic.Editor.ViewModels;

/// <summary>
/// Mob Page 的视图模型
/// </summary>
public sealed class MobPageViewModel : PageViewModelBase<MobCardViewModel, Mob>
{
    protected override string NewItemName { get; } = "New_Mob";
}
