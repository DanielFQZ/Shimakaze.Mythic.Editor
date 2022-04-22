using Shimakaze.Mythic.Editor.ViewModels;

namespace Shimakaze.Mythic.Editor.Views.Pages;
/// <summary>
/// ItemPage.xaml 的交互逻辑
/// </summary>
public partial class ItemPage : Page
{
    public ItemPage()
    {
        InitializeComponent();
        this.UidI18nInitialize();
        DataContext = new ItemPageViewModel();
    }
}