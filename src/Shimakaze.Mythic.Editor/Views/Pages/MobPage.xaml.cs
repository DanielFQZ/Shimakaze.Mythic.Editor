using Shimakaze.Mythic.Editor.ViewModels;

namespace Shimakaze.Mythic.Editor.Views.Pages;
/// <summary>
/// MobPage.xaml 的交互逻辑
/// </summary>
public partial class MobPage : Page
{
    public MobPage()
    {
        InitializeComponent();
        this.UidI18nInitialize();
        DataContext = new MobPageViewModel();
    }
}