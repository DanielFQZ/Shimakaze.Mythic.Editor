using Shimakaze.Mythic.Editor.ViewModels;

namespace Shimakaze.Mythic.Editor.Views.Pages;
/// <summary>
/// RandomSpawnPage.xaml 的交互逻辑
/// </summary>
public partial class RandomSpawnPage : Page
{
    public RandomSpawnPage()
    {
        InitializeComponent();
        this.UidI18nInitialize();
        DataContext = new RandomSpawnPageViewModel();
    }
}