using Shimakaze.Mythic.Editor.Models;

namespace Shimakaze.Mythic.Editor.ViewModels;

public class DisguiseCardViewModel : ISubViewModel<Disguise>, INotifyPropertyChanged
{
    public string? Type { get; set; } = string.Empty;

    public string? Skin { get; set; } = string.Empty;

    public string? Player { get; set; } = string.Empty;

#pragma warning disable CS0067 // 从不使用事件
    public event PropertyChangedEventHandler? PropertyChanged;
#pragma warning restore CS0067 // 从不使用事件

    private DisguiseCardViewModel() { }
    public DisguiseCardViewModel(Disguise? model) : this()
    {
        if (model is not null)
        {
            Type = model.Type;
            Skin = model.Skin;
            Player = model.Player; 
        }
    }
    public static ISubViewModel<Disguise> CreateEmpty() => new DisguiseCardViewModel();

    public static ISubViewModel<Disguise> FromModel(Disguise model) => new DisguiseCardViewModel(model);

    public Disguise ToModel() => new()
    {
        Type = Type,
        Skin = Skin,
        Player = Player,
    };
}