using System.Collections.ObjectModel;

using Microsoft.Win32;

using Shimakaze.Mythic.Editor.Services;

using WPFUI.Common;
using WPFUI.Taskbar;

namespace Shimakaze.Mythic.Editor.ViewModels;

/// <summary>
/// * Page 的视图模型
/// </summary>
public abstract class PageViewModelBase<TViewModel, TModel> : INotifyPropertyChanged
    where TModel : class
    where TViewModel : class, INameViewModel<TModel>, INotifyPropertyChanged
{
    /// <summary>
    /// 文件对话框过滤器常量
    /// </summary>
    private const string FILE_FILTER = "Yaml File|*.yaml;*.yml;*.eyml;*.eyaml;*.cff|All|*.*";
    private const string ITEM_CLONE_TEMPLATE = "{0}_Clone";
    protected abstract string NewItemName { get; }

    /// <summary>
    /// 当前打开的文件的路径
    /// </summary>
    public string FilePath { get; private set; } = string.Empty;

    /// <summary>
    /// Spawner 集合
    /// </summary>
    public ObservableCollection<INameViewModel<TModel>>? List { get; set; }

    /// <summary>
    /// 被选中的Spawner
    /// </summary>
    public INameViewModel<TModel>? SelectedItem { get; set; }

    /// <summary>
    /// 新建文件时执行的命令
    /// </summary>
    public ICommand NewFileCommand { get; }
    /// <summary>
    /// 打开文件时执行的命令
    /// </summary>
    public ICommand OpenFileCommand { get; }
    /// <summary>
    /// 保存文件时执行的命令
    /// </summary>
    public ICommand SaveFileCommand { get; }
    /// <summary>
    /// 另存为文件时执行的命令
    /// </summary>
    public ICommand SaveAsFileCommand { get; }

    /// <summary>
    /// 新建Spawner时执行的命令
    /// </summary>
    public ICommand NewItemCommand { get; }
    /// <summary>
    /// 删除Spawner时执行的命令
    /// </summary>
    public ICommand DropItemCommand { get; }
    /// <summary>
    /// 克隆Spawner时执行的命令
    /// </summary>
    public ICommand CopyItemCommand { get; }

    /// <summary>
    /// 用于通知属性被修改的事件
    /// </summary>
#pragma warning disable CS0067 // 从不使用事件
    public event PropertyChangedEventHandler? PropertyChanged;
#pragma warning restore CS0067 // 从不使用事件

    /// <summary>
    /// 构造函数, 用于初始化对象
    /// </summary>
    public PageViewModelBase()
    {
        // 中继命令
        // WPF 自带的路由命令不适合用于MVVM模式
        // see https://docs.microsoft.com/en-us/archive/msdn-magazine/2014/november/mvvm-wpf-commanding-with-the-state-machine-pattern
        NewFileCommand = new RelayCommand(ExecuteNewFileCommand);
        OpenFileCommand = new RelayCommand(ExecuteOpenFileCommand);
        SaveFileCommand = new RelayCommand(ExecuteSaveFileCommand, () => List is not null && List.Count is not 0);
        SaveAsFileCommand = new RelayCommand(ExecuteSaveAsFileCommand, () => List is not null && List.Count is not 0);

        NewItemCommand = new RelayCommand(ExecuteNewItemCommand);
        DropItemCommand = new RelayCommand(ExecuteDropItemCommand, () => SelectedItem is not null);
        CopyItemCommand = new RelayCommand(ExecuteCopyItemCommand, () => SelectedItem is not null);
    }
    /// <summary>
    /// 新建Spawner
    /// </summary>
    private void ExecuteNewItemCommand() => List?.Add(SelectedItem = TViewModel.CreateEmpty(NewItemName));
    /// <summary>
    /// 移除选中的Spawner
    /// </summary>
    private void ExecuteDropItemCommand() => List?.Remove(SelectedItem!);
    /// <summary>
    /// 克隆选中的Spawner
    /// </summary>
    private void ExecuteCopyItemCommand()
        => List?.Add(SelectedItem = TViewModel.FromModel(
            string.Format(ITEM_CLONE_TEMPLATE, SelectedItem!.Name),
            SelectedItem.ToModel()));

    /// <summary>
    /// 新建文件
    /// </summary>
    private void ExecuteNewFileCommand() => List = new() { (SelectedItem = TViewModel.CreateEmpty(NewItemName)) };

    /// <summary>
    /// 打开文件
    /// </summary>
    private void ExecuteOpenFileCommand()
    {
        // 设置任务栏图标状态为未知时长进度
        Progress.SetState(ProgressState.Indeterminate);
        try
        {
            // 创建一个打开文件对话框
            OpenFileDialog ofd = new()
            {
                Filter = FILE_FILTER
            };
            // 设置任务栏图标状态为暂停
            Progress.SetState(ProgressState.Paused);
            // 如果对话框成功返回
            if (ofd.ShowDialog() is true)
            {
                // 设置任务栏图标状态为未知时长进度
                Progress.SetState(ProgressState.Indeterminate);
                // 设置文件路径并读取文件
                ReadFromFile(FilePath = ofd.FileName);
            }
            // 设置任务栏图标状态为无状态
            Progress.SetState(ProgressState.None);
        }
        catch
        {
            // 设置任务栏图标状态为出现错误
            Progress.SetState(ProgressState.Error);
            // 将未经处理的异常重新抛出
            throw;
        }
    }
    /// <summary>
    /// 保存文件
    /// </summary>
    private void ExecuteSaveFileCommand()
    {
        if (string.IsNullOrEmpty(FilePath))
        {
            ExecuteSaveAsFileCommand();
            return;
        }
        // 设置任务栏图标状态为未知时长进度
        Progress.SetState(ProgressState.Indeterminate);
        try
        {
            // 写到文件
            WriteToFile(FilePath);

            // 设置任务栏图标状态为无状态
            Progress.SetState(ProgressState.None);
        }
        catch
        {
            // 设置任务栏图标状态为出现错误
            Progress.SetState(ProgressState.Error);
            // 将未经处理的异常重新抛出
            throw;
        }
    }
    /// <summary>
    /// 另存为文件
    /// </summary>
    private void ExecuteSaveAsFileCommand()
    {
        // 设置任务栏图标状态为未知时长进度
        Progress.SetState(ProgressState.Indeterminate);
        try
        {
            // 创建一个保存文件对话框
            SaveFileDialog sfd = new()
            {
                Filter = FILE_FILTER
            };
            // 设置任务栏图标状态为暂停
            Progress.SetState(ProgressState.Paused);
            // 如果对话框成功返回
            if (sfd.ShowDialog() is true)
            {
                // 设置任务栏图标状态为未知时长进度
                Progress.SetState(ProgressState.Indeterminate);
                // 重设文件路径并写到文件
                WriteToFile(FilePath = sfd.FileName);
            }
            // 设置任务栏图标状态为无状态
            Progress.SetState(ProgressState.None);
        }
        catch
        {
            // 设置任务栏图标状态为出现错误
            Progress.SetState(ProgressState.Error);
            // 将未经处理的异常重新抛出
            throw;
        }
    }

    /// <summary>
    /// 从路径读取文件
    /// </summary>
    /// <param name="path">文件路径</param>
    private void ReadFromFile(string path)
    {
        using StreamReader reader = new(File.OpenRead(path));
        List = new(FileService.ReadFrom<TModel>(reader)
            .Select(i => TViewModel.FromModel(i.Key, i.Value)));

        SelectedItem = List.First();
    }


    /// <summary>
    /// 写出文件到路径
    /// </summary>
    /// <param name="path"></param>
    private void WriteToFile(string path)
    {
        // null检查, 防止出现System.NullReferenceException
        if (List is null)
            return;

        // 创建StreamWriter用于写入, 使用using在不再需要时自动释放无法被GC回收的资源
        using StreamWriter writer = new(File.Create(path));
        // 将List转换成Map(Dictionary)后调用SpawnerIO.WriteTo写入
        FileService.WriteTo(List.ToDictionary(i => i.Name, i => i.ToModel()), writer);
    }
}