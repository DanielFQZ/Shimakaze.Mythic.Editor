namespace Shimakaze.Mythic.Editor.Models;
public sealed record class Spawner
{
    public string? MobName { get; set; } 
    public string? World { get; set; } 
    public string? SpawnerGroup { get; set; }
    public decimal? X { get; set; }
    public decimal? Y { get; set; }
    public decimal? Z { get; set; }
    public decimal? Radius { get; set; }
    public decimal? RadiusY { get; set; }
    public bool? UseTimer { get; set; }
    public decimal? MaxMobs { get; set; }
    public decimal? MobLevel { get; set; }
    public decimal? MobsPerSpawn { get; set; }
    public decimal? Cooldown { get; set; }
    public decimal? CooldownTimer { get; set; }
    public decimal? Warmup { get; set; }
    public decimal? WarmupTimer { get; set; }
    public bool? CheckForPlayers { get; set; }
    public decimal? ActivationRange { get; set; }
    public decimal? LeashRange { get; set; }
    public bool? HealOnLeash { get; set; }
    public bool? ResetThreatOnLeash { get; set; }
    public bool? ShowFlames { get; set; }
    public bool? Breakable { get; set; }
    public List<string>? Conditions { get; set; }
    public decimal? ActiveMobs { get; set; }
}