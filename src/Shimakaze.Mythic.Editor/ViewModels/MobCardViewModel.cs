using System.Collections.ObjectModel;
using System.DirectoryServices.ActiveDirectory;
using System.Runtime.CompilerServices;
using System.Windows.Markup;

using Shimakaze.Mythic.Editor.Models;

using WPFUI.Common;

namespace Shimakaze.Mythic.Editor.ViewModels;
public class MobCardViewModel : INameViewModel<Mob>, INotifyPropertyChanged
{
    public string Name { get; set; } = string.Empty;
    public string? Type { get; set; }
    public string? Display { get; set; }
    public decimal? Health { get; set; }
    public decimal? Damage { get; set; }
    public decimal? Armor { get; set; }
    public string? Faction { get; set; }
    public string? Mount { get; set; }
    public MobOptionsCardViewModel? Options { get; set; }
    public ModulesCardViewModel? Modules { get; set; }
    public DisguiseCardViewModel? Disguise { get; set; }

    public event PropertyChangedEventHandler? PropertyChanged;

    private MobCardViewModel()
    {
        AIGoalSelectors = new();

        ApplyAIGoalSelectorCommand = new RelayCommand(ExecuteApplyAIGoalSelectorCommand);
        AddAIGoalSelectorCommand = new RelayCommand(ExecuteAddAIGoalSelectorCommand);
        DropAIGoalSelectorCommand = new RelayCommand(ExecuteDropAIGoalSelectorCommand);
        MoveAIGoalSelectorUpCommand = new RelayCommand(ExecuteMoveAIGoalSelectorUpCommand,
            () => AIGoalSelectors is not null && SelectedAIGoalSelector is not null && AIGoalSelectors.IndexOf(SelectedAIGoalSelector) > 0);
        MoveAIGoalSelectorDownCommand = new RelayCommand(ExecuteMoveAIGoalSelectorDownCommand,
            () => AIGoalSelectors is not null && SelectedAIGoalSelector is not null && AIGoalSelectors.IndexOf(SelectedAIGoalSelector) < AIGoalSelectors.Count - 1);
        AITargetSelectors = new();

        ApplyAITargetSelectorCommand = new RelayCommand(ExecuteApplyAITargetSelectorCommand);
        AddAITargetSelectorCommand = new RelayCommand(ExecuteAddAITargetSelectorCommand);
        DropAITargetSelectorCommand = new RelayCommand(ExecuteDropAITargetSelectorCommand);
        MoveAITargetSelectorUpCommand = new RelayCommand(ExecuteMoveAITargetSelectorUpCommand,
            () => AITargetSelectors is not null && SelectedAITargetSelector is not null && AITargetSelectors.IndexOf(SelectedAITargetSelector) > 0);
        MoveAITargetSelectorDownCommand = new RelayCommand(ExecuteMoveAITargetSelectorDownCommand,
            () => AITargetSelectors is not null && SelectedAITargetSelector is not null && AITargetSelectors.IndexOf(SelectedAITargetSelector) < AITargetSelectors.Count - 1);
        Drops = new();

        ApplyDropCommand = new RelayCommand(ExecuteApplyDropCommand);
        AddDropCommand = new RelayCommand(ExecuteAddDropCommand);
        DropDropCommand = new RelayCommand(ExecuteDropDropCommand);
        MoveDropUpCommand = new RelayCommand(ExecuteMoveDropUpCommand,
            () => Drops is not null && SelectedDrop is not null && Drops.IndexOf(SelectedDrop) > 0);
        MoveDropDownCommand = new RelayCommand(ExecuteMoveDropDownCommand,
            () => Drops is not null && SelectedDrop is not null && Drops.IndexOf(SelectedDrop) < Drops.Count - 1);
        DropsPerLevels = new();

        ApplyDropsPerLevelCommand = new RelayCommand(ExecuteApplyDropsPerLevelCommand);
        AddDropsPerLevelCommand = new RelayCommand(ExecuteAddDropsPerLevelCommand);
        DropDropsPerLevelCommand = new RelayCommand(ExecuteDropDropsPerLevelCommand);
        MoveDropsPerLevelUpCommand = new RelayCommand(ExecuteMoveDropsPerLevelUpCommand,
            () => DropsPerLevels is not null && SelectedDropsPerLevel is not null && DropsPerLevels.IndexOf(SelectedDropsPerLevel) > 0);
        MoveDropsPerLevelDownCommand = new RelayCommand(ExecuteMoveDropsPerLevelDownCommand,
            () => DropsPerLevels is not null && SelectedDropsPerLevel is not null && DropsPerLevels.IndexOf(SelectedDropsPerLevel) < DropsPerLevels.Count - 1);
        DamageModifiers = new();

        ApplyDamageModifierCommand = new RelayCommand(ExecuteApplyDamageModifierCommand);
        AddDamageModifierCommand = new RelayCommand(ExecuteAddDamageModifierCommand);
        DropDamageModifierCommand = new RelayCommand(ExecuteDropDamageModifierCommand);
        MoveDamageModifierUpCommand = new RelayCommand(ExecuteMoveDamageModifierUpCommand,
            () => DamageModifiers is not null && SelectedDamageModifier is not null && DamageModifiers.IndexOf(SelectedDamageModifier) > 0);
        MoveDamageModifierDownCommand = new RelayCommand(ExecuteMoveDamageModifierDownCommand,
            () => DamageModifiers is not null && SelectedDamageModifier is not null && DamageModifiers.IndexOf(SelectedDamageModifier) < DamageModifiers.Count - 1);
        Equipments = new();

        ApplyEquipmentCommand = new RelayCommand(ExecuteApplyEquipmentCommand);
        AddEquipmentCommand = new RelayCommand(ExecuteAddEquipmentCommand);
        DropEquipmentCommand = new RelayCommand(ExecuteDropEquipmentCommand);
        MoveEquipmentUpCommand = new RelayCommand(ExecuteMoveEquipmentUpCommand,
            () => Equipments is not null && SelectedEquipment is not null && Equipments.IndexOf(SelectedEquipment) > 0);
        MoveEquipmentDownCommand = new RelayCommand(ExecuteMoveEquipmentDownCommand,
            () => Equipments is not null && SelectedEquipment is not null && Equipments.IndexOf(SelectedEquipment) < Equipments.Count - 1);
        KillMessages = new();

        ApplyKillMessageCommand = new RelayCommand(ExecuteApplyKillMessageCommand);
        AddKillMessageCommand = new RelayCommand(ExecuteAddKillMessageCommand);
        DropKillMessageCommand = new RelayCommand(ExecuteDropKillMessageCommand);
        MoveKillMessageUpCommand = new RelayCommand(ExecuteMoveKillMessageUpCommand,
            () => KillMessages is not null && SelectedKillMessage is not null && KillMessages.IndexOf(SelectedKillMessage) > 0);
        MoveKillMessageDownCommand = new RelayCommand(ExecuteMoveKillMessageDownCommand,
            () => KillMessages is not null && SelectedKillMessage is not null && KillMessages.IndexOf(SelectedKillMessage) < KillMessages.Count - 1);
        LevelModifiers = new();

        ApplyLevelModifierCommand = new RelayCommand(ExecuteApplyLevelModifierCommand);
        AddLevelModifierCommand = new RelayCommand(ExecuteAddLevelModifierCommand);
        DropLevelModifierCommand = new RelayCommand(ExecuteDropLevelModifierCommand);
        MoveLevelModifierUpCommand = new RelayCommand(ExecuteMoveLevelModifierUpCommand,
            () => LevelModifiers is not null && SelectedLevelModifier is not null && LevelModifiers.IndexOf(SelectedLevelModifier) > 0);
        MoveLevelModifierDownCommand = new RelayCommand(ExecuteMoveLevelModifierDownCommand,
            () => LevelModifiers is not null && SelectedLevelModifier is not null && LevelModifiers.IndexOf(SelectedLevelModifier) < LevelModifiers.Count - 1);
        Skills = new();

        ApplySkillCommand = new RelayCommand(ExecuteApplySkillCommand);
        AddSkillCommand = new RelayCommand(ExecuteAddSkillCommand);
        DropSkillCommand = new RelayCommand(ExecuteDropSkillCommand);
        MoveSkillUpCommand = new RelayCommand(ExecuteMoveSkillUpCommand,
            () => Skills is not null && SelectedSkill is not null && Skills.IndexOf(SelectedSkill) > 0);
        MoveSkillDownCommand = new RelayCommand(ExecuteMoveSkillDownCommand,
            () => Skills is not null && SelectedSkill is not null && Skills.IndexOf(SelectedSkill) < Skills.Count - 1);
    }
    private MobCardViewModel(string name) : this() => Name = name;
    private MobCardViewModel(string name, Mob model) : this(name)
    {
        Type = model.Type;
        Display = model.Display;
        Health = model.Health;
        Damage = model.Damage;
        Armor = model.Armor;
        Faction = model.Faction;
        Mount = model.Mount;
        Options = new(model.Options);
        Modules = new(model.Modules);
        Disguise = new (model.Disguise);

        if (model.AIGoalSelectors is not null)
            AIGoalSelectors = new(model.AIGoalSelectors);

        if (model.AITargetSelectors is not null)
            AITargetSelectors = new(model.AITargetSelectors);

        if (model.Drops is not null)
            Drops = new(model.Drops);

        if (model.DropsPerLevel is not null)
            DropsPerLevels = new(model.DropsPerLevel);

        if (model.DamageModifiers is not null)
            DamageModifiers = new(model.DamageModifiers);

        if (model.Equipment is not null)
            Equipments = new(model.Equipment);

        if (model.KillMessages is not null)
            KillMessages = new(model.KillMessages);

        if (model.LevelModifiers is not null)
            LevelModifiers = new(model.LevelModifiers);

        if (model.Skills is not null)
            Skills = new(model.Skills);
    }


    public static INameViewModel<Mob> CreateEmpty(string name) => new MobCardViewModel(name);

    public static INameViewModel<Mob> FromModel(string name, Mob model) => new MobCardViewModel(name, model);
    private void OnPropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new(propertyName));

    public Mob ToModel() => new()
    {
        Type = Type,
        Display = Display,
        Health = Health,
        Damage = Damage,
        Armor = Armor,
        Faction = Faction,
        Mount = Mount,
        Options = Options?.ToModel(),
        Modules = Modules?.ToModel(),
        Disguise = Disguise?.ToModel(),

        AIGoalSelectors = AIGoalSelectors.ToList(),
        AITargetSelectors = AITargetSelectors.ToList(),
        Drops = Drops.ToList(),
        DropsPerLevel = DropsPerLevels.ToList(),
        DamageModifiers = DamageModifiers.ToList(),
        Equipment = Equipments.ToList(),
        KillMessages = KillMessages.ToList(),
        LevelModifiers = LevelModifiers.ToList(),
        Skills = Skills.ToList(),
    };


    public ObservableCollection<string> AIGoalSelectors { get; set; }
    private string? _selectedAIGoalSelector;
    public string? SelectedAIGoalSelector
    {
        get => _selectedAIGoalSelector; set
        {
            EditAIGoalSelector = _selectedAIGoalSelector = value;
            OnPropertyChanged();
        }
    }
    public string? EditAIGoalSelector { get; set; }
    public ICommand ApplyAIGoalSelectorCommand { get; }
    public ICommand AddAIGoalSelectorCommand { get; }
    public ICommand DropAIGoalSelectorCommand { get; }
    public ICommand MoveAIGoalSelectorUpCommand { get; }
    public ICommand MoveAIGoalSelectorDownCommand { get; }
    private void ExecuteApplyAIGoalSelectorCommand()
    {
        // 当选中的内容和编辑框内容都不为空时修改内容
        if (!string.IsNullOrWhiteSpace(SelectedAIGoalSelector) && !string.IsNullOrWhiteSpace(EditAIGoalSelector))
        {
            // 必须先保存编辑框内容, 当集合被修改时选中项会变, 编辑框内容也会跟着变
            var tmp = EditAIGoalSelector;
            AIGoalSelectors.Remove(SelectedAIGoalSelector);
            AIGoalSelectors.Add(tmp);
            SelectedAIGoalSelector = EditAIGoalSelector = tmp;
        }
    }

    private void ExecuteAddAIGoalSelectorCommand()
    {
        // 如果编辑框里有内容就直接添加编辑框里的内容
        if (!string.IsNullOrWhiteSpace(EditAIGoalSelector))
        {
            AIGoalSelectors.Add(EditAIGoalSelector);
            SelectedAIGoalSelector = EditAIGoalSelector;
        }
    }
    private void ExecuteDropAIGoalSelectorCommand()
    {
        // 只要 SelectedAIGoalSelector 不是 null 就可以删, 避免遇到删不掉空白字符串的情况
        if (SelectedAIGoalSelector is not null)
            AIGoalSelectors.Remove(SelectedAIGoalSelector);
    }

    private void ExecuteMoveAIGoalSelectorUpCommand()
    {
        if (SelectedAIGoalSelector is not null)
        {
            int index = AIGoalSelectors.IndexOf(SelectedAIGoalSelector);
            AIGoalSelectors.Move(index--, index);
        }
    }
    private void ExecuteMoveAIGoalSelectorDownCommand()
    {
        if (SelectedAIGoalSelector is not null)
        {
            int index = AIGoalSelectors.IndexOf(SelectedAIGoalSelector);
            AIGoalSelectors.Move(index++, index);
        }
    }
    public ObservableCollection<string> AITargetSelectors { get; set; }
    private string? _selectedAITargetSelector;
    public string? SelectedAITargetSelector
    {
        get => _selectedAITargetSelector; set
        {
            EditAITargetSelector = _selectedAITargetSelector = value;
            OnPropertyChanged();
        }
    }
    public string? EditAITargetSelector { get; set; }
    public ICommand ApplyAITargetSelectorCommand { get; }
    public ICommand AddAITargetSelectorCommand { get; }
    public ICommand DropAITargetSelectorCommand { get; }
    public ICommand MoveAITargetSelectorUpCommand { get; }
    public ICommand MoveAITargetSelectorDownCommand { get; }
    private void ExecuteApplyAITargetSelectorCommand()
    {
        // 当选中的内容和编辑框内容都不为空时修改内容
        if (!string.IsNullOrWhiteSpace(SelectedAITargetSelector) && !string.IsNullOrWhiteSpace(EditAITargetSelector))
        {
            // 必须先保存编辑框内容, 当集合被修改时选中项会变, 编辑框内容也会跟着变
            var tmp = EditAITargetSelector;
            AITargetSelectors.Remove(SelectedAITargetSelector);
            AITargetSelectors.Add(tmp);
            SelectedAITargetSelector = EditAITargetSelector = tmp;
        }
    }

    private void ExecuteAddAITargetSelectorCommand()
    {
        // 如果编辑框里有内容就直接添加编辑框里的内容
        if (!string.IsNullOrWhiteSpace(EditAITargetSelector))
        {
            AITargetSelectors.Add(EditAITargetSelector);
            SelectedAITargetSelector = EditAITargetSelector;
        }
    }
    private void ExecuteDropAITargetSelectorCommand()
    {
        // 只要 SelectedAITargetSelector 不是 null 就可以删, 避免遇到删不掉空白字符串的情况
        if (SelectedAITargetSelector is not null)
            AITargetSelectors.Remove(SelectedAITargetSelector);
    }

    private void ExecuteMoveAITargetSelectorUpCommand()
    {
        if (SelectedAITargetSelector is not null)
        {
            int index = AITargetSelectors.IndexOf(SelectedAITargetSelector);
            AITargetSelectors.Move(index--, index);
        }
    }
    private void ExecuteMoveAITargetSelectorDownCommand()
    {
        if (SelectedAITargetSelector is not null)
        {
            int index = AITargetSelectors.IndexOf(SelectedAITargetSelector);
            AITargetSelectors.Move(index++, index);
        }
    }
    public ObservableCollection<string> Drops { get; set; }
    private string? _selectedDrop;
    public string? SelectedDrop
    {
        get => _selectedDrop; set
        {
            EditDrop = _selectedDrop = value;
            OnPropertyChanged();
        }
    }
    public string? EditDrop { get; set; }
    public ICommand ApplyDropCommand { get; }
    public ICommand AddDropCommand { get; }
    public ICommand DropDropCommand { get; }
    public ICommand MoveDropUpCommand { get; }
    public ICommand MoveDropDownCommand { get; }
    private void ExecuteApplyDropCommand()
    {
        // 当选中的内容和编辑框内容都不为空时修改内容
        if (!string.IsNullOrWhiteSpace(SelectedDrop) && !string.IsNullOrWhiteSpace(EditDrop))
        {
            // 必须先保存编辑框内容, 当集合被修改时选中项会变, 编辑框内容也会跟着变
            var tmp = EditDrop;
            Drops.Remove(SelectedDrop);
            Drops.Add(tmp);
            SelectedDrop = EditDrop = tmp;
        }
    }

    private void ExecuteAddDropCommand()
    {
        // 如果编辑框里有内容就直接添加编辑框里的内容
        if (!string.IsNullOrWhiteSpace(EditDrop))
        {
            Drops.Add(EditDrop);
            SelectedDrop = EditDrop;
        }
    }
    private void ExecuteDropDropCommand()
    {
        // 只要 SelectedDrop 不是 null 就可以删, 避免遇到删不掉空白字符串的情况
        if (SelectedDrop is not null)
            Drops.Remove(SelectedDrop);
    }

    private void ExecuteMoveDropUpCommand()
    {
        if (SelectedDrop is not null)
        {
            int index = Drops.IndexOf(SelectedDrop);
            Drops.Move(index--, index);
        }
    }
    private void ExecuteMoveDropDownCommand()
    {
        if (SelectedDrop is not null)
        {
            int index = Drops.IndexOf(SelectedDrop);
            Drops.Move(index++, index);
        }
    }
    public ObservableCollection<string> DropsPerLevels { get; set; }
    private string? _selectedDropsPerLevel;
    public string? SelectedDropsPerLevel
    {
        get => _selectedDropsPerLevel; set
        {
            EditDropsPerLevel = _selectedDropsPerLevel = value;
            OnPropertyChanged();
        }
    }
    public string? EditDropsPerLevel { get; set; }
    public ICommand ApplyDropsPerLevelCommand { get; }
    public ICommand AddDropsPerLevelCommand { get; }
    public ICommand DropDropsPerLevelCommand { get; }
    public ICommand MoveDropsPerLevelUpCommand { get; }
    public ICommand MoveDropsPerLevelDownCommand { get; }
    private void ExecuteApplyDropsPerLevelCommand()
    {
        // 当选中的内容和编辑框内容都不为空时修改内容
        if (!string.IsNullOrWhiteSpace(SelectedDropsPerLevel) && !string.IsNullOrWhiteSpace(EditDropsPerLevel))
        {
            // 必须先保存编辑框内容, 当集合被修改时选中项会变, 编辑框内容也会跟着变
            var tmp = EditDropsPerLevel;
            DropsPerLevels.Remove(SelectedDropsPerLevel);
            DropsPerLevels.Add(tmp);
            SelectedDropsPerLevel = EditDropsPerLevel = tmp;
        }
    }

    private void ExecuteAddDropsPerLevelCommand()
    {
        // 如果编辑框里有内容就直接添加编辑框里的内容
        if (!string.IsNullOrWhiteSpace(EditDropsPerLevel))
        {
            DropsPerLevels.Add(EditDropsPerLevel);
            SelectedDropsPerLevel = EditDropsPerLevel;
        }
    }
    private void ExecuteDropDropsPerLevelCommand()
    {
        // 只要 SelectedDropsPerLevel 不是 null 就可以删, 避免遇到删不掉空白字符串的情况
        if (SelectedDropsPerLevel is not null)
            DropsPerLevels.Remove(SelectedDropsPerLevel);
    }

    private void ExecuteMoveDropsPerLevelUpCommand()
    {
        if (SelectedDropsPerLevel is not null)
        {
            int index = DropsPerLevels.IndexOf(SelectedDropsPerLevel);
            DropsPerLevels.Move(index--, index);
        }
    }
    private void ExecuteMoveDropsPerLevelDownCommand()
    {
        if (SelectedDropsPerLevel is not null)
        {
            int index = DropsPerLevels.IndexOf(SelectedDropsPerLevel);
            DropsPerLevels.Move(index++, index);
        }
    }
    public ObservableCollection<string> DamageModifiers { get; set; }
    private string? _selectedDamageModifier;
    public string? SelectedDamageModifier
    {
        get => _selectedDamageModifier; set
        {
            EditDamageModifier = _selectedDamageModifier = value;
            OnPropertyChanged();
        }
    }
    public string? EditDamageModifier { get; set; }
    public ICommand ApplyDamageModifierCommand { get; }
    public ICommand AddDamageModifierCommand { get; }
    public ICommand DropDamageModifierCommand { get; }
    public ICommand MoveDamageModifierUpCommand { get; }
    public ICommand MoveDamageModifierDownCommand { get; }
    private void ExecuteApplyDamageModifierCommand()
    {
        // 当选中的内容和编辑框内容都不为空时修改内容
        if (!string.IsNullOrWhiteSpace(SelectedDamageModifier) && !string.IsNullOrWhiteSpace(EditDamageModifier))
        {
            // 必须先保存编辑框内容, 当集合被修改时选中项会变, 编辑框内容也会跟着变
            var tmp = EditDamageModifier;
            DamageModifiers.Remove(SelectedDamageModifier);
            DamageModifiers.Add(tmp);
            SelectedDamageModifier = EditDamageModifier = tmp;
        }
    }

    private void ExecuteAddDamageModifierCommand()
    {
        // 如果编辑框里有内容就直接添加编辑框里的内容
        if (!string.IsNullOrWhiteSpace(EditDamageModifier))
        {
            DamageModifiers.Add(EditDamageModifier);
            SelectedDamageModifier = EditDamageModifier;
        }
    }
    private void ExecuteDropDamageModifierCommand()
    {
        // 只要 SelectedDamageModifier 不是 null 就可以删, 避免遇到删不掉空白字符串的情况
        if (SelectedDamageModifier is not null)
            DamageModifiers.Remove(SelectedDamageModifier);
    }

    private void ExecuteMoveDamageModifierUpCommand()
    {
        if (SelectedDamageModifier is not null)
        {
            int index = DamageModifiers.IndexOf(SelectedDamageModifier);
            DamageModifiers.Move(index--, index);
        }
    }
    private void ExecuteMoveDamageModifierDownCommand()
    {
        if (SelectedDamageModifier is not null)
        {
            int index = DamageModifiers.IndexOf(SelectedDamageModifier);
            DamageModifiers.Move(index++, index);
        }
    }
    public ObservableCollection<string> Equipments { get; set; }
    private string? _selectedEquipment;
    public string? SelectedEquipment
    {
        get => _selectedEquipment; set
        {
            EditEquipment = _selectedEquipment = value;
            OnPropertyChanged();
        }
    }
    public string? EditEquipment { get; set; }
    public ICommand ApplyEquipmentCommand { get; }
    public ICommand AddEquipmentCommand { get; }
    public ICommand DropEquipmentCommand { get; }
    public ICommand MoveEquipmentUpCommand { get; }
    public ICommand MoveEquipmentDownCommand { get; }
    private void ExecuteApplyEquipmentCommand()
    {
        // 当选中的内容和编辑框内容都不为空时修改内容
        if (!string.IsNullOrWhiteSpace(SelectedEquipment) && !string.IsNullOrWhiteSpace(EditEquipment))
        {
            // 必须先保存编辑框内容, 当集合被修改时选中项会变, 编辑框内容也会跟着变
            var tmp = EditEquipment;
            Equipments.Remove(SelectedEquipment);
            Equipments.Add(tmp);
            SelectedEquipment = EditEquipment = tmp;
        }
    }

    private void ExecuteAddEquipmentCommand()
    {
        // 如果编辑框里有内容就直接添加编辑框里的内容
        if (!string.IsNullOrWhiteSpace(EditEquipment))
        {
            Equipments.Add(EditEquipment);
            SelectedEquipment = EditEquipment;
        }
    }
    private void ExecuteDropEquipmentCommand()
    {
        // 只要 SelectedEquipment 不是 null 就可以删, 避免遇到删不掉空白字符串的情况
        if (SelectedEquipment is not null)
            Equipments.Remove(SelectedEquipment);
    }

    private void ExecuteMoveEquipmentUpCommand()
    {
        if (SelectedEquipment is not null)
        {
            int index = Equipments.IndexOf(SelectedEquipment);
            Equipments.Move(index--, index);
        }
    }
    private void ExecuteMoveEquipmentDownCommand()
    {
        if (SelectedEquipment is not null)
        {
            int index = Equipments.IndexOf(SelectedEquipment);
            Equipments.Move(index++, index);
        }
    }
    public ObservableCollection<string> KillMessages { get; set; }
    private string? _selectedKillMessage;
    public string? SelectedKillMessage
    {
        get => _selectedKillMessage; set
        {
            EditKillMessage = _selectedKillMessage = value;
            OnPropertyChanged();
        }
    }
    public string? EditKillMessage { get; set; }
    public ICommand ApplyKillMessageCommand { get; }
    public ICommand AddKillMessageCommand { get; }
    public ICommand DropKillMessageCommand { get; }
    public ICommand MoveKillMessageUpCommand { get; }
    public ICommand MoveKillMessageDownCommand { get; }
    private void ExecuteApplyKillMessageCommand()
    {
        // 当选中的内容和编辑框内容都不为空时修改内容
        if (!string.IsNullOrWhiteSpace(SelectedKillMessage) && !string.IsNullOrWhiteSpace(EditKillMessage))
        {
            // 必须先保存编辑框内容, 当集合被修改时选中项会变, 编辑框内容也会跟着变
            var tmp = EditKillMessage;
            KillMessages.Remove(SelectedKillMessage);
            KillMessages.Add(tmp);
            SelectedKillMessage = EditKillMessage = tmp;
        }
    }

    private void ExecuteAddKillMessageCommand()
    {
        // 如果编辑框里有内容就直接添加编辑框里的内容
        if (!string.IsNullOrWhiteSpace(EditKillMessage))
        {
            KillMessages.Add(EditKillMessage);
            SelectedKillMessage = EditKillMessage;
        }
    }
    private void ExecuteDropKillMessageCommand()
    {
        // 只要 SelectedKillMessage 不是 null 就可以删, 避免遇到删不掉空白字符串的情况
        if (SelectedKillMessage is not null)
            KillMessages.Remove(SelectedKillMessage);
    }

    private void ExecuteMoveKillMessageUpCommand()
    {
        if (SelectedKillMessage is not null)
        {
            int index = KillMessages.IndexOf(SelectedKillMessage);
            KillMessages.Move(index--, index);
        }
    }
    private void ExecuteMoveKillMessageDownCommand()
    {
        if (SelectedKillMessage is not null)
        {
            int index = KillMessages.IndexOf(SelectedKillMessage);
            KillMessages.Move(index++, index);
        }
    }
    public ObservableCollection<string> LevelModifiers { get; set; }
    private string? _selectedLevelModifier;
    public string? SelectedLevelModifier
    {
        get => _selectedLevelModifier; set
        {
            EditLevelModifier = _selectedLevelModifier = value;
            OnPropertyChanged();
        }
    }
    public string? EditLevelModifier { get; set; }
    public ICommand ApplyLevelModifierCommand { get; }
    public ICommand AddLevelModifierCommand { get; }
    public ICommand DropLevelModifierCommand { get; }
    public ICommand MoveLevelModifierUpCommand { get; }
    public ICommand MoveLevelModifierDownCommand { get; }
    private void ExecuteApplyLevelModifierCommand()
    {
        // 当选中的内容和编辑框内容都不为空时修改内容
        if (!string.IsNullOrWhiteSpace(SelectedLevelModifier) && !string.IsNullOrWhiteSpace(EditLevelModifier))
        {
            // 必须先保存编辑框内容, 当集合被修改时选中项会变, 编辑框内容也会跟着变
            var tmp = EditLevelModifier;
            LevelModifiers.Remove(SelectedLevelModifier);
            LevelModifiers.Add(tmp);
            SelectedLevelModifier = EditLevelModifier = tmp;
        }
    }

    private void ExecuteAddLevelModifierCommand()
    {
        // 如果编辑框里有内容就直接添加编辑框里的内容
        if (!string.IsNullOrWhiteSpace(EditLevelModifier))
        {
            LevelModifiers.Add(EditLevelModifier);
            SelectedLevelModifier = EditLevelModifier;
        }
    }
    private void ExecuteDropLevelModifierCommand()
    {
        // 只要 SelectedLevelModifier 不是 null 就可以删, 避免遇到删不掉空白字符串的情况
        if (SelectedLevelModifier is not null)
            LevelModifiers.Remove(SelectedLevelModifier);
    }

    private void ExecuteMoveLevelModifierUpCommand()
    {
        if (SelectedLevelModifier is not null)
        {
            int index = LevelModifiers.IndexOf(SelectedLevelModifier);
            LevelModifiers.Move(index--, index);
        }
    }
    private void ExecuteMoveLevelModifierDownCommand()
    {
        if (SelectedLevelModifier is not null)
        {
            int index = LevelModifiers.IndexOf(SelectedLevelModifier);
            LevelModifiers.Move(index++, index);
        }
    }
    public ObservableCollection<string> Skills { get; set; }
    private string? _selectedSkill;
    public string? SelectedSkill
    {
        get => _selectedSkill; set
        {
            EditSkill = _selectedSkill = value;
            OnPropertyChanged();
        }
    }
    public string? EditSkill { get; set; }
    public ICommand ApplySkillCommand { get; }
    public ICommand AddSkillCommand { get; }
    public ICommand DropSkillCommand { get; }
    public ICommand MoveSkillUpCommand { get; }
    public ICommand MoveSkillDownCommand { get; }
    private void ExecuteApplySkillCommand()
    {
        // 当选中的内容和编辑框内容都不为空时修改内容
        if (!string.IsNullOrWhiteSpace(SelectedSkill) && !string.IsNullOrWhiteSpace(EditSkill))
        {
            // 必须先保存编辑框内容, 当集合被修改时选中项会变, 编辑框内容也会跟着变
            var tmp = EditSkill;
            Skills.Remove(SelectedSkill);
            Skills.Add(tmp);
            SelectedSkill = EditSkill = tmp;
        }
    }

    private void ExecuteAddSkillCommand()
    {
        // 如果编辑框里有内容就直接添加编辑框里的内容
        if (!string.IsNullOrWhiteSpace(EditSkill))
        {
            Skills.Add(EditSkill);
            SelectedSkill = EditSkill;
        }
    }
    private void ExecuteDropSkillCommand()
    {
        // 只要 SelectedSkill 不是 null 就可以删, 避免遇到删不掉空白字符串的情况
        if (SelectedSkill is not null)
            Skills.Remove(SelectedSkill);
    }

    private void ExecuteMoveSkillUpCommand()
    {
        if (SelectedSkill is not null)
        {
            int index = Skills.IndexOf(SelectedSkill);
            Skills.Move(index--, index);
        }
    }
    private void ExecuteMoveSkillDownCommand()
    {
        if (SelectedSkill is not null)
        {
            int index = Skills.IndexOf(SelectedSkill);
            Skills.Move(index++, index);
        }
    }

}
