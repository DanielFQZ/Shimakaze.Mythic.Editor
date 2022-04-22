using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

using Shimakaze.Mythic.Editor.Models;

using WPFUI.Common;

namespace Shimakaze.Mythic.Editor.ViewModels;

public sealed class DroptableCardViewModel : INameViewModel<Droptable>, INotifyPropertyChanged
{
    private string? _selectedCondition;
    private string? _selectedDrop;
    public string Name { get; set; } = string.Empty;
    public ObservableCollection<string> Conditions { get; set; }
    public ObservableCollection<string> Drops { get; set; }


    public string? SelectedCondition
    {
        get => _selectedCondition; set
        {
            EditCondition = _selectedCondition = value;
            OnPropertyChanged();
        }
    }
    public string? EditCondition { get; set; }
    public ICommand ApplyConditionCommand { get; }
    public ICommand AddConditionCommand { get; }
    public ICommand DropConditionCommand { get; }
    public ICommand MoveConditionUpCommand { get; }
    public ICommand MoveConditionDownCommand { get; }
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

    public event PropertyChangedEventHandler? PropertyChanged;

    private DroptableCardViewModel()
    {
        Conditions = new();
        Drops = new();

        ApplyConditionCommand = new RelayCommand(ExecuteApplyConditionCommand);
        AddConditionCommand = new RelayCommand(ExecuteAddConditionCommand);
        DropConditionCommand = new RelayCommand(ExecuteDropConditionCommand);
        MoveConditionUpCommand = new RelayCommand(ExecuteMoveConditionUpCommand,
            () => Conditions is not null && SelectedCondition is not null && Conditions.IndexOf(SelectedCondition) > 0);
        MoveConditionDownCommand = new RelayCommand(ExecuteMoveConditionDownCommand,
            () => Conditions is not null && SelectedCondition is not null && Conditions.IndexOf(SelectedCondition) < Conditions.Count - 1);

        ApplyDropCommand = new RelayCommand(ExecuteApplyDropCommand);
        AddDropCommand = new RelayCommand(ExecuteAddDropCommand);
        DropDropCommand = new RelayCommand(ExecuteDropDropCommand);
        MoveDropUpCommand = new RelayCommand(ExecuteMoveDropUpCommand,
            () => Drops is not null && SelectedDrop is not null && Drops.IndexOf(SelectedDrop) > 0);
        MoveDropDownCommand = new RelayCommand(ExecuteMoveDropDownCommand,
            () => Drops is not null && SelectedDrop is not null && Drops.IndexOf(SelectedDrop) < Drops.Count - 1);

    }
    public DroptableCardViewModel(string name) : this() => Name = name;
    public DroptableCardViewModel(string name, Droptable model) : this(name)
    {
        if (model.Conditions is not null)
            Conditions = new(model.Conditions);
        if (model.Drops is not null)
            Drops = new(model.Drops);
    }




    private void ExecuteApplyConditionCommand()
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

    private void ExecuteAddConditionCommand()
    {
        // 如果编辑框里有内容就直接添加编辑框里的内容
        if (!string.IsNullOrWhiteSpace(EditCondition))
        {
            Conditions.Add(EditCondition);
            SelectedCondition = EditCondition;
        }
    }
    private void ExecuteDropConditionCommand()
    {
        // 只要 SelectedCondition 不是 null 就可以删, 避免遇到删不掉空白字符串的情况
        if (SelectedCondition is not null)
            Conditions.Remove(SelectedCondition);
    }

    private void ExecuteMoveConditionUpCommand()
    {
        if (SelectedCondition is not null)
        {
            int index = Conditions.IndexOf(SelectedCondition);
            Conditions.Move(index--, index);
        }
    }
    private void ExecuteMoveConditionDownCommand()
    {
        if (SelectedCondition is not null)
        {
            int index = Conditions.IndexOf(SelectedCondition);
            Conditions.Move(index++, index);
        }
    }
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


    public static INameViewModel<Droptable> CreateEmpty(string name) => new DroptableCardViewModel(name);

    public static INameViewModel<Droptable> FromModel(string name, Droptable model) => new DroptableCardViewModel(name, model);
    private void OnPropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new(propertyName));

    public Droptable ToModel() => new()
    {
        Conditions = Conditions.ToList(),
        Drops = Drops.ToList(),
    };
}
