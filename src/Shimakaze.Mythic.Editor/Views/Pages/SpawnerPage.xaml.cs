using Shimakaze.Mythic.Editor.ViewModels;

namespace Shimakaze.Mythic.Editor.Views.Pages;
/// <summary>
/// SpawnerPage.xaml 的交互逻辑
/// </summary>
public partial class SpawnerPage : Page
{
    public SpawnerPage()
    {
        InitializeComponent();
        this.UidI18nInitialize();
        DataContext = new SpawnerPageViewModel();
    }
}