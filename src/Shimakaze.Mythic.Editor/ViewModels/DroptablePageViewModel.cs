using Shimakaze.Mythic.Editor.Models;

namespace Shimakaze.Mythic.Editor.ViewModels;

/// <summary>
/// Droptable Page 的视图模型
/// </summary>
public sealed class DroptablePageViewModel : PageViewModelBase<DroptableCardViewModel, Droptable>
{
    protected override string NewItemName { get; } = "New_Droptable";
}
