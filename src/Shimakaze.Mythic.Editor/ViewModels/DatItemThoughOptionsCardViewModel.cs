using Shimakaze.Mythic.Editor.Models;

namespace Shimakaze.Mythic.Editor.ViewModels;

public class DatItemThoughOptionsCardViewModel : ISubViewModel<DatItemThoughOptions>, INotifyPropertyChanged
{
    public string? Color { get; set; } = string.Empty;

    public decimal? Damage { get; set; }

    public decimal? Health { get; set; }

    public decimal? FollowRange { get; set; }

    public decimal? KnockbackResistance { get; set; }

    public decimal? MovementSpeed { get; set; }

    public bool? HideFlags { get; set; }

    public bool? Unbreakable { get; set; }

#pragma warning disable CS0067 // 从不使用事件
    public event PropertyChangedEventHandler? PropertyChanged;
#pragma warning restore CS0067 // 从不使用事件

    public DatItemThoughOptionsCardViewModel() { }
    public DatItemThoughOptionsCardViewModel(DatItemThoughOptions? model) : this()
    {
        if (model is not null)
        {
            Color = model.Color;
            Damage = model.Damage;
            Health = model.Health;
            FollowRange = model.FollowRange;
            KnockbackResistance = model.KnockbackResistance;
            MovementSpeed = model.MovementSpeed;
            HideFlags = model.HideFlags;
            Unbreakable = model.Unbreakable;
        }
    }

    public DatItemThoughOptions ToModel() => new()
    {
        Color = Color,
        Damage = Damage,
        Health = Health,
        FollowRange = FollowRange,
        KnockbackResistance = KnockbackResistance,
        MovementSpeed = MovementSpeed,
        HideFlags = HideFlags,
        Unbreakable = Unbreakable,
    };
}