namespace Shimakaze.Mythic.Editor.Models;
public class Mob
{
    public string? Type { get; set; }
    public string? Display { get; set; }
    public decimal? Health { get; set; }
    public decimal? Damage { get; set; }
    public decimal? Armor { get; set; }
    public string? Faction { get; set; }
    public string? Mount { get; set; }
    public MobOptions? Options { get; set; }
    public Modules? Modules { get; set; }
    public List<string>? AIGoalSelectors { get; set; }
    public List<string>? AITargetSelectors { get; set; }
    public List<string>? Drops { get; set; }
    public List<string>? DropsPerLevel { get; set; }
    public List<string>? DamageModifiers { get; set; }
    public List<string>? Equipment { get; set; }
    public List<string>? KillMessages { get; set; }
    public List<string>? LevelModifiers { get; set; }
    public Disguise? Disguise { get; set; }
    public List<string>? Skills { get; set; }
}
