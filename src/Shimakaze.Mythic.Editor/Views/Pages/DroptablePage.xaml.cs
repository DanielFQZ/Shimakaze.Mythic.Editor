using Shimakaze.Mythic.Editor.ViewModels;

namespace Shimakaze.Mythic.Editor.Views.Pages;
/// <summary>
/// DroptablePage.xaml 的交互逻辑
/// </summary>
public partial class DroptablePage : Page
{
    public DroptablePage()
    {
        InitializeComponent();
        this.UidI18nInitialize();
        DataContext = new DroptablePageViewModel();
    }
}