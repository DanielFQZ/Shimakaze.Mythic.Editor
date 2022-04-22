using WPFUI.Appearance;

namespace Shimakaze.Mythic.Editor.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        this.UidI18nInitialize();
        RootNavigation.Items.Each(i => (i as UIElement)?.UidI18nInitialize());

        // 调用初始化窗口
        Loaded += (o, e) => Watcher.Watch(this, AutoBackgroundType);
}

    public static BackgroundType AutoBackgroundType
    {
        get
        {
            var version = Environment.OSVersion.Version;
            return version.Major switch
            {
                10 => version.Build switch
                {
                    >= 22523 => BackgroundType.Tabbed,
                    >= 22000 => BackgroundType.Mica,
                    >= 17134 => BackgroundType.Acrylic,
                    _ => BackgroundType.Unknown
                },
                _ => BackgroundType.Unknown
            };
        }
    }
}
