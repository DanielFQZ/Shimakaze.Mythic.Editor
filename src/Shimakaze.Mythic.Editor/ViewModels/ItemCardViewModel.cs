using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

using Shimakaze.Mythic.Editor.Models;

using WPFUI.Common;

namespace Shimakaze.Mythic.Editor.ViewModels;

public sealed class ItemCardViewModel : INameViewModel<Item>, INotifyPropertyChanged
{
    public string Name { get; set; } = string.Empty;
    public event PropertyChangedEventHandler? PropertyChanged;

    public string? Id { get; set; }
    public decimal? Data { get; set; }
    public string? Display { get; set; }
    public decimal? Amount { get; set; }

    public DatItemThoughOptionsCardViewModel? Options { get; set; }

    private ItemCardViewModel()
    {
        Options = new();

        Lores = new();

        ApplyLoreCommand = new RelayCommand(ExecuteApplyLoreCommand);
        AddLoreCommand = new RelayCommand(ExecuteAddLoreCommand);
        DropLoreCommand = new RelayCommand(ExecuteDropLoreCommand);
        MoveLoreUpCommand = new RelayCommand(ExecuteMoveLoreUpCommand,
            () => Lores is not null && SelectedLore is not null && Lores.IndexOf(SelectedLore) > 0);
        MoveLoreDownCommand = new RelayCommand(ExecuteMoveLoreDownCommand,
            () => Lores is not null && SelectedLore is not null && Lores.IndexOf(SelectedLore) < Lores.Count - 1);
        Enchantments = new();

        ApplyEnchantmentCommand = new RelayCommand(ExecuteApplyEnchantmentCommand);
        AddEnchantmentCommand = new RelayCommand(ExecuteAddEnchantmentCommand);
        DropEnchantmentCommand = new RelayCommand(ExecuteDropEnchantmentCommand);
        MoveEnchantmentUpCommand = new RelayCommand(ExecuteMoveEnchantmentUpCommand,
            () => Enchantments is not null && SelectedEnchantment is not null && Enchantments.IndexOf(SelectedEnchantment) > 0);
        MoveEnchantmentDownCommand = new RelayCommand(ExecuteMoveEnchantmentDownCommand,
            () => Enchantments is not null && SelectedEnchantment is not null && Enchantments.IndexOf(SelectedEnchantment) < Enchantments.Count - 1);
        PotionEffects = new();

        ApplyPotionEffectCommand = new RelayCommand(ExecuteApplyPotionEffectCommand);
        AddPotionEffectCommand = new RelayCommand(ExecuteAddPotionEffectCommand);
        DropPotionEffectCommand = new RelayCommand(ExecuteDropPotionEffectCommand);
        MovePotionEffectUpCommand = new RelayCommand(ExecuteMovePotionEffectUpCommand,
            () => PotionEffects is not null && SelectedPotionEffect is not null && PotionEffects.IndexOf(SelectedPotionEffect) > 0);
        MovePotionEffectDownCommand = new RelayCommand(ExecuteMovePotionEffectDownCommand,
            () => PotionEffects is not null && SelectedPotionEffect is not null && PotionEffects.IndexOf(SelectedPotionEffect) < PotionEffects.Count - 1);
        Hides = new();

        ApplyHideCommand = new RelayCommand(ExecuteApplyHideCommand);
        AddHideCommand = new RelayCommand(ExecuteAddHideCommand);
        DropHideCommand = new RelayCommand(ExecuteDropHideCommand);
        MoveHideUpCommand = new RelayCommand(ExecuteMoveHideUpCommand,
            () => Hides is not null && SelectedHide is not null && Hides.IndexOf(SelectedHide) > 0);
        MoveHideDownCommand = new RelayCommand(ExecuteMoveHideDownCommand,
            () => Hides is not null && SelectedHide is not null && Hides.IndexOf(SelectedHide) < Hides.Count - 1);
    }
    public ItemCardViewModel(string name) : this() => Name = name;
    public ItemCardViewModel(string name, Item model) : this(name)
    {
        Id = model.Id;
        Data = model.Data;
        Display = model.Display;
        Amount = model.Amount;
        Options = new(model.Options);

        if (model.Lore is not null)
            Lores = new(model.Lore);

        if (model.Enchantments is not null)
            Enchantments = new(model.Enchantments);

        if (model.PotionEffects is not null)
            PotionEffects = new(model.PotionEffects);

        if (model.Hide is not null)
            Hides = new(model.Hide);
    }


    public ObservableCollection<string> Lores { get; set; }
    private string? _selectedLore;
    public string? SelectedLore
    {
        get => _selectedLore; set
        {
            EditLore = _selectedLore = value;
            OnPropertyChanged();
        }
    }
    public string? EditLore { get; set; }
    public ICommand ApplyLoreCommand { get; }
    public ICommand AddLoreCommand { get; }
    public ICommand DropLoreCommand { get; }
    public ICommand MoveLoreUpCommand { get; }
    public ICommand MoveLoreDownCommand { get; }
    private void ExecuteApplyLoreCommand()
    {
        // 当选中的内容和编辑框内容都不为空时修改内容
        if (!string.IsNullOrWhiteSpace(SelectedLore) && !string.IsNullOrWhiteSpace(EditLore))
        {
            // 必须先保存编辑框内容, 当集合被修改时选中项会变, 编辑框内容也会跟着变
            var tmp = EditLore;
            Lores.Remove(SelectedLore);
            Lores.Add(tmp);
            SelectedLore = EditLore = tmp;
        }
    }

    private void ExecuteAddLoreCommand()
    {
        // 如果编辑框里有内容就直接添加编辑框里的内容
        if (!string.IsNullOrWhiteSpace(EditLore))
        {
            Lores.Add(EditLore);
            SelectedLore = EditLore;
        }
    }
    private void ExecuteDropLoreCommand()
    {
        // 只要 SelectedLore 不是 null 就可以删, 避免遇到删不掉空白字符串的情况
        if (SelectedLore is not null)
            Lores.Remove(SelectedLore);
    }

    private void ExecuteMoveLoreUpCommand()
    {
        if (SelectedLore is not null)
        {
            int index = Lores.IndexOf(SelectedLore);
            Lores.Move(index--, index);
        }
    }
    private void ExecuteMoveLoreDownCommand()
    {
        if (SelectedLore is not null)
        {
            int index = Lores.IndexOf(SelectedLore);
            Lores.Move(index++, index);
        }
    }
    public ObservableCollection<string> Enchantments { get; set; }
    private string? _selectedEnchantment;
    public string? SelectedEnchantment
    {
        get => _selectedEnchantment; set
        {
            EditEnchantment = _selectedEnchantment = value;
            OnPropertyChanged();
        }
    }
    public string? EditEnchantment { get; set; }
    public ICommand ApplyEnchantmentCommand { get; }
    public ICommand AddEnchantmentCommand { get; }
    public ICommand DropEnchantmentCommand { get; }
    public ICommand MoveEnchantmentUpCommand { get; }
    public ICommand MoveEnchantmentDownCommand { get; }
    private void ExecuteApplyEnchantmentCommand()
    {
        // 当选中的内容和编辑框内容都不为空时修改内容
        if (!string.IsNullOrWhiteSpace(SelectedEnchantment) && !string.IsNullOrWhiteSpace(EditEnchantment))
        {
            // 必须先保存编辑框内容, 当集合被修改时选中项会变, 编辑框内容也会跟着变
            var tmp = EditEnchantment;
            Enchantments.Remove(SelectedEnchantment);
            Enchantments.Add(tmp);
            SelectedEnchantment = EditEnchantment = tmp;
        }
    }

    private void ExecuteAddEnchantmentCommand()
    {
        // 如果编辑框里有内容就直接添加编辑框里的内容
        if (!string.IsNullOrWhiteSpace(EditEnchantment))
        {
            Enchantments.Add(EditEnchantment);
            SelectedEnchantment = EditEnchantment;
        }
    }
    private void ExecuteDropEnchantmentCommand()
    {
        // 只要 SelectedEnchantment 不是 null 就可以删, 避免遇到删不掉空白字符串的情况
        if (SelectedEnchantment is not null)
            Enchantments.Remove(SelectedEnchantment);
    }

    private void ExecuteMoveEnchantmentUpCommand()
    {
        if (SelectedEnchantment is not null)
        {
            int index = Enchantments.IndexOf(SelectedEnchantment);
            Enchantments.Move(index--, index);
        }
    }
    private void ExecuteMoveEnchantmentDownCommand()
    {
        if (SelectedEnchantment is not null)
        {
            int index = Enchantments.IndexOf(SelectedEnchantment);
            Enchantments.Move(index++, index);
        }
    }
    public ObservableCollection<string> PotionEffects { get; set; }
    private string? _selectedPotionEffect;
    public string? SelectedPotionEffect
    {
        get => _selectedPotionEffect; set
        {
            EditPotionEffect = _selectedPotionEffect = value;
            OnPropertyChanged();
        }
    }
    public string? EditPotionEffect { get; set; }
    public ICommand ApplyPotionEffectCommand { get; }
    public ICommand AddPotionEffectCommand { get; }
    public ICommand DropPotionEffectCommand { get; }
    public ICommand MovePotionEffectUpCommand { get; }
    public ICommand MovePotionEffectDownCommand { get; }
    private void ExecuteApplyPotionEffectCommand()
    {
        // 当选中的内容和编辑框内容都不为空时修改内容
        if (!string.IsNullOrWhiteSpace(SelectedPotionEffect) && !string.IsNullOrWhiteSpace(EditPotionEffect))
        {
            // 必须先保存编辑框内容, 当集合被修改时选中项会变, 编辑框内容也会跟着变
            var tmp = EditPotionEffect;
            PotionEffects.Remove(SelectedPotionEffect);
            PotionEffects.Add(tmp);
            SelectedPotionEffect = EditPotionEffect = tmp;
        }
    }

    private void ExecuteAddPotionEffectCommand()
    {
        // 如果编辑框里有内容就直接添加编辑框里的内容
        if (!string.IsNullOrWhiteSpace(EditPotionEffect))
        {
            PotionEffects.Add(EditPotionEffect);
            SelectedPotionEffect = EditPotionEffect;
        }
    }
    private void ExecuteDropPotionEffectCommand()
    {
        // 只要 SelectedPotionEffect 不是 null 就可以删, 避免遇到删不掉空白字符串的情况
        if (SelectedPotionEffect is not null)
            PotionEffects.Remove(SelectedPotionEffect);
    }

    private void ExecuteMovePotionEffectUpCommand()
    {
        if (SelectedPotionEffect is not null)
        {
            int index = PotionEffects.IndexOf(SelectedPotionEffect);
            PotionEffects.Move(index--, index);
        }
    }
    private void ExecuteMovePotionEffectDownCommand()
    {
        if (SelectedPotionEffect is not null)
        {
            int index = PotionEffects.IndexOf(SelectedPotionEffect);
            PotionEffects.Move(index++, index);
        }
    }
    public ObservableCollection<string> Hides { get; set; }
    private string? _selectedHide;
    public string? SelectedHide
    {
        get => _selectedHide; set
        {
            EditHide = _selectedHide = value;
            OnPropertyChanged();
        }
    }
    public string? EditHide { get; set; }
    public ICommand ApplyHideCommand { get; }
    public ICommand AddHideCommand { get; }
    public ICommand DropHideCommand { get; }
    public ICommand MoveHideUpCommand { get; }
    public ICommand MoveHideDownCommand { get; }
    private void ExecuteApplyHideCommand()
    {
        // 当选中的内容和编辑框内容都不为空时修改内容
        if (!string.IsNullOrWhiteSpace(SelectedHide) && !string.IsNullOrWhiteSpace(EditHide))
        {
            // 必须先保存编辑框内容, 当集合被修改时选中项会变, 编辑框内容也会跟着变
            var tmp = EditHide;
            Hides.Remove(SelectedHide);
            Hides.Add(tmp);
            SelectedHide = EditHide = tmp;
        }
    }

    private void ExecuteAddHideCommand()
    {
        // 如果编辑框里有内容就直接添加编辑框里的内容
        if (!string.IsNullOrWhiteSpace(EditHide))
        {
            Hides.Add(EditHide);
            SelectedHide = EditHide;
        }
    }
    private void ExecuteDropHideCommand()
    {
        // 只要 SelectedHide 不是 null 就可以删, 避免遇到删不掉空白字符串的情况
        if (SelectedHide is not null)
            Hides.Remove(SelectedHide);
    }

    private void ExecuteMoveHideUpCommand()
    {
        if (SelectedHide is not null)
        {
            int index = Hides.IndexOf(SelectedHide);
            Hides.Move(index--, index);
        }
    }
    private void ExecuteMoveHideDownCommand()
    {
        if (SelectedHide is not null)
        {
            int index = Hides.IndexOf(SelectedHide);
            Hides.Move(index++, index);
        }
    }





    public static INameViewModel<Item> CreateEmpty(string name) => new ItemCardViewModel(name);

    public static INameViewModel<Item> FromModel(string name, Item model) => new ItemCardViewModel(name, model);
    private void OnPropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new(propertyName));

    public Item ToModel() => new()
    {
        Id = Id,
        Data = Data,
        Display = Display,
        Amount = Amount,
        Options = Options?.ToModel(),
        Lore = Lores.ToList(),
        Enchantments = Enchantments.ToList(),
        PotionEffects = PotionEffects.ToList(),
        Hide = Hides.ToList(),
    };
}
