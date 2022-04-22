namespace Shimakaze.Mythic.Editor.Models;
public class Item
{
    public string? Id { get; set; }
    public decimal? Data { get; set; }
    public string? Display { get; set; }
    public List<string>? Lore { get; set; }
    public decimal? Amount { get; set; }
    public DatItemThoughOptions? Options { get; set; }
    public List<string>? Enchantments { get; set; }
    public List<string>? PotionEffects { get; set; }
    public List<string>? Hide { get; set; }
}
