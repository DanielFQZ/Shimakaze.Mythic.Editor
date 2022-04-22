namespace Shimakaze.Mythic.Editor.Models;
public class RandomSpawn
{
    public string? Action { get; set; } 
    public string? Type { get; set; } 
    public decimal? Level { get; set; }
    public decimal? Chance { get; set; }
    public decimal? Priority { get; set; }
    public bool? UseWorldScaling { get; set; }
    public string? Worlds { get; set; } 
}
