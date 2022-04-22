using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

using Shimakaze.Mythic.Editor.Models;

using WPFUI.Common;

namespace Shimakaze.Mythic.Editor.ViewModels;

public sealed class SpawnerCardViewModel : INameViewModel<Spawner>, INotifyPropertyChanged
{
    private string? _selectedCondition;

    public string Name { get; set; } = string.Empty;
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
    public ObservableCollection<string> Conditions { get; set; }
    public decimal? ActiveMobs { get; set; }

    public string? SelectedCondition
    {
        get => _selectedCondition; set
        {
            EditCondition = _selectedCondition = value;
            OnPropertyChanged();
        }
    }
    public string? EditCondition { get; set; }
    public ICommand ApplyCommand { get; }
    public ICommand AddCommand { get; }
    public ICommand DropCommand { get; }
    public ICommand MoveUpCommand { get; }
    public ICommand MoveDownCommand { get; }

    private SpawnerCardViewModel()
    {
        ApplyCommand = new RelayCommand(ExecuteApplyCommand);
        AddCommand = new RelayCommand(ExecuteAddCommand);
        DropCommand = new RelayCommand(ExecuteDropCommand);
        MoveUpCommand = new RelayCommand(ExecuteMoveUpCommand,
            () => Conditions is not null && SelectedCondition is not null && Conditions.IndexOf(SelectedCondition) > 0);
        MoveDownCommand = new RelayCommand(ExecuteMoveDownCommand,
            () => Conditions is not null && SelectedCondition is not null && Conditions.IndexOf(SelectedCondition) < Conditions.Count - 1);
        Conditions = new();
    }

    public SpawnerCardViewModel(string name) : this() => Name = name;

    public SpawnerCardViewModel(string name, Spawner model) : this(name)
    {
        MobName = model.MobName;
        World = model.World;
        SpawnerGroup = model.SpawnerGroup;
        X = model.X;
        Y = model.Y;
        Z = model.Z;
        Radius = model.Radius;
        RadiusY = model.RadiusY;
        UseTimer = model.UseTimer;
        MaxMobs = model.MaxMobs;
        MobLevel = model.MobLevel;
        MobsPerSpawn = model.MobsPerSpawn;
        Cooldown = model.Cooldown;
        CooldownTimer = model.CooldownTimer;
        Warmup = model.Warmup;
        WarmupTimer = model.WarmupTimer;
        CheckForPlayers = model.CheckForPlayers;
        ActivationRange = model.ActivationRange;
        LeashRange = model.LeashRange;
        HealOnLeash = model.HealOnLeash;
        ResetThreatOnLeash = model.ResetThreatOnLeash;
        ShowFlames = model.ShowFlames;
        Breakable = model.Breakable;
        Conditions = new(model.Conditions);
        ActiveMobs = model.ActiveMobs;

        EditCondition = SelectedCondition = Conditions.FirstOrDefault();
    }

    private void ExecuteApplyCommand()
    {
        // 当选中的内容和编辑框内容都不为空时修改内容
        if (!string.IsNullOrWhiteSpace(SelectedCondition) && !string.IsNullOrWhiteSpace(EditCondition))
        {
            // 必须先保存编辑框内容, 当集合被修改时选中项会变, 编辑框内容也会跟着变
            var tmp = EditCondition;
            Conditions.Remove(SelectedCondition);
            Conditions.Add(tmp);
            SelectedCondition = EditCondition = tmp;
        }
    }

    private void ExecuteAddCommand()
    {
        // 如果编辑框里有内容就直接添加编辑框里的内容
        if (!string.IsNullOrWhiteSpace(EditCondition))
        {
            Conditions.Add(EditCondition);
            SelectedCondition = EditCondition;
        }
    }
    private void ExecuteDropCommand()
    {
        // 只要 SelectedCondition 不是 null 就可以删, 避免遇到删不掉空白字符串的情况
        if (SelectedCondition is not null)
            Conditions.Remove(SelectedCondition);
    }

    private void ExecuteMoveUpCommand()
    {
        if (SelectedCondition is not null)
        {
            int index = Conditions.IndexOf(SelectedCondition);
            Conditions.Move(index--, index);
        }
    }
    private void ExecuteMoveDownCommand()
    {
        if (SelectedCondition is not null)
        {
            int index = Conditions.IndexOf(SelectedCondition);
            Conditions.Move(index++, index);
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new(propertyName));

    /// <summary>
    /// 将VM对象转换成序列化器用的简单模型
    /// </summary>
    /// <returns>模型对象</returns>
    public Spawner ToModel() => new()
    {
        MobName = MobName,
        World = World,
        SpawnerGroup = SpawnerGroup,
        X = X,
        Y = Y,
        Z = Z,
        Radius = Radius,
        RadiusY = RadiusY,
        UseTimer = UseTimer,
        MaxMobs = MaxMobs,
        MobLevel = MobLevel,
        MobsPerSpawn = MobsPerSpawn,
        Cooldown = Cooldown,
        CooldownTimer = CooldownTimer,
        Warmup = Warmup,
        WarmupTimer = WarmupTimer,
        CheckForPlayers = CheckForPlayers,
        ActivationRange = ActivationRange,
        LeashRange = LeashRange,
        HealOnLeash = HealOnLeash,
        ResetThreatOnLeash = ResetThreatOnLeash,
        ShowFlames = ShowFlames,
        Breakable = Breakable,
        Conditions = Conditions.ToList(),
        ActiveMobs = ActiveMobs,
    };

    public static INameViewModel<Spawner> FromModel(string name, Spawner model) => new SpawnerCardViewModel(name, model);

    public static INameViewModel<Spawner> CreateEmpty(string name) => new SpawnerCardViewModel(name);
}
