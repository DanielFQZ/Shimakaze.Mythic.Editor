namespace Shimakaze.Mythic.Editor.Models;

public class MobOptions
{
    public bool? PreventOtherDrops { get; set; }
    public bool? PreventItemPickup { get; set; }
    public bool? Despawn { get; set; }
    public decimal? KnockbackResistance { get; set; }
    public decimal? MovementSpeed { get; set; }
}
