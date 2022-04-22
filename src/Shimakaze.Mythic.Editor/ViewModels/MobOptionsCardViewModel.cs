using Shimakaze.Mythic.Editor.Models;

namespace Shimakaze.Mythic.Editor.ViewModels;

public class MobOptionsCardViewModel : ISubViewModel<MobOptions>, INotifyPropertyChanged
{
    public bool? PreventOtherDrops { get; set; }

    public bool? PreventItemPickup { get; set; }

    public bool? Despawn { get; set; }

    public decimal? KnockbackResistance { get; set; }

    public decimal? MovementSpeed { get; set; }

#pragma warning disable CS0067 // 从不使用事件
    public event PropertyChangedEventHandler? PropertyChanged;
#pragma warning restore CS0067 // 从不使用事件

    private MobOptionsCardViewModel() { }
    public MobOptionsCardViewModel(MobOptions? model) : this()
    {
        if (model is not null)
        {
            PreventOtherDrops = model.PreventOtherDrops;
            PreventItemPickup = model.PreventItemPickup;
            Despawn = model.Despawn;
            KnockbackResistance = model.KnockbackResistance;
            MovementSpeed = model.MovementSpeed; 
        }
    }
    public static ISubViewModel<MobOptions> CreateEmpty() => new MobOptionsCardViewModel();

    public static ISubViewModel<MobOptions> FromModel(MobOptions model) => new MobOptionsCardViewModel(model);

    public MobOptions ToModel() => new()
    {
        PreventOtherDrops = PreventOtherDrops,
        PreventItemPickup = PreventItemPickup,
        Despawn = Despawn,
        KnockbackResistance = KnockbackResistance,
        MovementSpeed = MovementSpeed,
    };
}
