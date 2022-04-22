namespace Shimakaze.Mythic.Editor;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private static App? s_instance;

    internal static App Instance => s_instance ?? throw new Exception("发生了预料之外的错误");

    public App()
    {
        s_instance = this;
    }
}
