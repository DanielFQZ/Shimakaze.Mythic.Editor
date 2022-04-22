using Shimakaze.Mythic.Editor.Models;

namespace Shimakaze.Mythic.Editor.ViewModels;

public class ModulesCardViewModel : ISubViewModel<Modules>, INotifyPropertyChanged
{
    public bool? ThreatTable { get; set; }

    public bool? ImmunityTable { get; set; }

#pragma warning disable CS0067 // 从不使用事件
    public event PropertyChangedEventHandler? PropertyChanged;
#pragma warning restore CS0067 // 从不使用事件

    private ModulesCardViewModel() { }
    public ModulesCardViewModel(Modules? model) : this()
    {
        if (model is not null)
        {
            ThreatTable = model.ThreatTable;
            ImmunityTable = model.ImmunityTable; 
        }
    }
    public static ISubViewModel<Modules> CreateEmpty() => new ModulesCardViewModel();

    public static ISubViewModel<Modules> FromModel(Modules model) => new ModulesCardViewModel(model);

    public Modules ToModel() => new()
    {
        ThreatTable = ThreatTable,
        ImmunityTable = ImmunityTable,
    };
}
