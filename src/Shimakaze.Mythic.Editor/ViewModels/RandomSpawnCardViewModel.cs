using Shimakaze.Mythic.Editor.Models;

namespace Shimakaze.Mythic.Editor.ViewModels;

public sealed class RandomSpawnCardViewModel : INameViewModel<RandomSpawn>, INotifyPropertyChanged
{
    public string Name { get; set; } = string.Empty;

    public string? Action { get; set; }

    public string? Type { get; set; }

    public decimal? Level { get; set; }

    public decimal? Chance { get; set; }

    public decimal? Priority { get; set; }

    public bool? UseWorldScaling { get; set; }

    public string? Worlds { get; set; }

#pragma warning disable CS0067 // 从不使用事件
    public event PropertyChangedEventHandler? PropertyChanged;
#pragma warning restore CS0067 // 从不使用事件

    private RandomSpawnCardViewModel() { }
    public RandomSpawnCardViewModel(string name) : this() => Name = name;
    public RandomSpawnCardViewModel(string name, RandomSpawn model) : this(name)
    {
        Action = model.Action;
        Type = model.Type;
        Level = model.Level;
        Chance = model.Chance;
        Priority = model.Priority;
        UseWorldScaling = model.UseWorldScaling;
        Worlds = model.Worlds;
    }

    public static INameViewModel<RandomSpawn> CreateEmpty(string name) => new RandomSpawnCardViewModel(name);

    public static INameViewModel<RandomSpawn> FromModel(string name, RandomSpawn model) => new RandomSpawnCardViewModel(name, model);

    public RandomSpawn ToModel() => new()
    {
        Action = Action,
        Type = Type,
        Level = Level,
        Chance = Chance,
        Priority = Priority,
        UseWorldScaling = UseWorldScaling,
        Worlds = Worlds,
    };
}
