using Shimakaze.Mythic.Editor.Models;

namespace Shimakaze.Mythic.Editor.ViewModels;

/// <summary>
/// Item Page 的视图模型
/// </summary>
public sealed class ItemPageViewModel : PageViewModelBase<ItemCardViewModel, Item>
{
    protected override string NewItemName { get; } = "New_Item";
}
