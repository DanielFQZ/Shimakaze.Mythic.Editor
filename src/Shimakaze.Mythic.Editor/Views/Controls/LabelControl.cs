namespace Shimakaze.Mythic.Editor.Views.Controls;

public sealed class LabelControl : ContentControl
{
    static LabelControl()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(LabelControl), new FrameworkPropertyMetadata(typeof(LabelControl)));
    }

    public string Label
    {
        get { return (string)GetValue(LabelProperty); }
        set { SetValue(LabelProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Header.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty LabelProperty =
        DependencyProperty.Register("Label", typeof(string), typeof(LabelControl));



    public GridLength LabelLength
    {
        get { return (GridLength)GetValue(LabelLengthProperty); }
        set { SetValue(LabelLengthProperty, value); }
    }

    // Using a DependencyProperty as the backing store for LabelLength.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty LabelLengthProperty =
        DependencyProperty.Register("LabelLength", typeof(GridLength), typeof(LabelControl), new PropertyMetadata(GridLength.Auto));


}