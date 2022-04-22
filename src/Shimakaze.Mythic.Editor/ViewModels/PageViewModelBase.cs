using System.Collections.ObjectModel;

using Microsoft.Win32;

using Shimakaze.Mythic.Editor.Services;

using WPFUI.Common;
using WPFUI.Taskbar;

namespace Shimakaze.Mythic.Editor.ViewModels;

/// <summary>
/// * Page ����ͼģ��
/// </summary>
public abstract class PageViewModelBase<TViewModel, TModel> : INotifyPropertyChanged
    where TModel : class
    where TViewModel : class, INameViewModel<TModel>, INotifyPropertyChanged
{
    /// <summary>
    /// �ļ��Ի������������
    /// </summary>
    private const string FILE_FILTER = "Yaml File|*.yaml;*.yml;*.eyml;*.eyaml;*.cff|All|*.*";
    private const string ITEM_CLONE_TEMPLATE = "{0}_Clone";
    protected abstract string NewItemName { get; }

    /// <summary>
    /// ��ǰ�򿪵��ļ���·��
    /// </summary>
    public string FilePath { get; private set; } = string.Empty;

    /// <summary>
    /// Spawner ����
    /// </summary>
    public ObservableCollection<INameViewModel<TModel>>? List { get; set; }

    /// <summary>
    /// ��ѡ�е�Spawner
    /// </summary>
    public INameViewModel<TModel>? SelectedItem { get; set; }

    /// <summary>
    /// �½��ļ�ʱִ�е�����
    /// </summary>
    public ICommand NewFileCommand { get; }
    /// <summary>
    /// ���ļ�ʱִ�е�����
    /// </summary>
    public ICommand OpenFileCommand { get; }
    /// <summary>
    /// �����ļ�ʱִ�е�����
    /// </summary>
    public ICommand SaveFileCommand { get; }
    /// <summary>
    /// ���Ϊ�ļ�ʱִ�е�����
    /// </summary>
    public ICommand SaveAsFileCommand { get; }

    /// <summary>
    /// �½�Spawnerʱִ�е�����
    /// </summary>
    public ICommand NewItemCommand { get; }
    /// <summary>
    /// ɾ��Spawnerʱִ�е�����
    /// </summary>
    public ICommand DropItemCommand { get; }
    /// <summary>
    /// ��¡Spawnerʱִ�е�����
    /// </summary>
    public ICommand CopyItemCommand { get; }

    /// <summary>
    /// ����֪ͨ���Ա��޸ĵ��¼�
    /// </summary>
#pragma warning disable CS0067 // �Ӳ�ʹ���¼�
    public event PropertyChangedEventHandler? PropertyChanged;
#pragma warning restore CS0067 // �Ӳ�ʹ���¼�

    /// <summary>
    /// ���캯��, ���ڳ�ʼ������
    /// </summary>
    public PageViewModelBase()
    {
        // �м�����
        // WPF �Դ���·������ʺ�����MVVMģʽ
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
    /// �½�Spawner
    /// </summary>
    private void ExecuteNewItemCommand() => List?.Add(SelectedItem = TViewModel.CreateEmpty(NewItemName));
    /// <summary>
    /// �Ƴ�ѡ�е�Spawner
    /// </summary>
    private void ExecuteDropItemCommand() => List?.Remove(SelectedItem!);
    /// <summary>
    /// ��¡ѡ�е�Spawner
    /// </summary>
    private void ExecuteCopyItemCommand()
        => List?.Add(SelectedItem = TViewModel.FromModel(
            string.Format(ITEM_CLONE_TEMPLATE, SelectedItem!.Name),
            SelectedItem.ToModel()));

    /// <summary>
    /// �½��ļ�
    /// </summary>
    private void ExecuteNewFileCommand() => List = new() { (SelectedItem = TViewModel.CreateEmpty(NewItemName)) };

    /// <summary>
    /// ���ļ�
    /// </summary>
    private void ExecuteOpenFileCommand()
    {
        // ����������ͼ��״̬Ϊδ֪ʱ������
        Progress.SetState(ProgressState.Indeterminate);
        try
        {
            // ����һ�����ļ��Ի���
            OpenFileDialog ofd = new()
            {
                Filter = FILE_FILTER
            };
            // ����������ͼ��״̬Ϊ��ͣ
            Progress.SetState(ProgressState.Paused);
            // ����Ի���ɹ�����
            if (ofd.ShowDialog() is true)
            {
                // ����������ͼ��״̬Ϊδ֪ʱ������
                Progress.SetState(ProgressState.Indeterminate);
                // �����ļ�·������ȡ�ļ�
                ReadFromFile(FilePath = ofd.FileName);
            }
            // ����������ͼ��״̬Ϊ��״̬
            Progress.SetState(ProgressState.None);
        }
        catch
        {
            // ����������ͼ��״̬Ϊ���ִ���
            Progress.SetState(ProgressState.Error);
            // ��δ��������쳣�����׳�
            throw;
        }
    }
    /// <summary>
    /// �����ļ�
    /// </summary>
    private void ExecuteSaveFileCommand()
    {
        if (string.IsNullOrEmpty(FilePath))
        {
            ExecuteSaveAsFileCommand();
            return;
        }
        // ����������ͼ��״̬Ϊδ֪ʱ������
        Progress.SetState(ProgressState.Indeterminate);
        try
        {
            // д���ļ�
            WriteToFile(FilePath);

            // ����������ͼ��״̬Ϊ��״̬
            Progress.SetState(ProgressState.None);
        }
        catch
        {
            // ����������ͼ��״̬Ϊ���ִ���
            Progress.SetState(ProgressState.Error);
            // ��δ��������쳣�����׳�
            throw;
        }
    }
    /// <summary>
    /// ���Ϊ�ļ�
    /// </summary>
    private void ExecuteSaveAsFileCommand()
    {
        // ����������ͼ��״̬Ϊδ֪ʱ������
        Progress.SetState(ProgressState.Indeterminate);
        try
        {
            // ����һ�������ļ��Ի���
            SaveFileDialog sfd = new()
            {
                Filter = FILE_FILTER
            };
            // ����������ͼ��״̬Ϊ��ͣ
            Progress.SetState(ProgressState.Paused);
            // ����Ի���ɹ�����
            if (sfd.ShowDialog() is true)
            {
                // ����������ͼ��״̬Ϊδ֪ʱ������
                Progress.SetState(ProgressState.Indeterminate);
                // �����ļ�·����д���ļ�
                WriteToFile(FilePath = sfd.FileName);
            }
            // ����������ͼ��״̬Ϊ��״̬
            Progress.SetState(ProgressState.None);
        }
        catch
        {
            // ����������ͼ��״̬Ϊ���ִ���
            Progress.SetState(ProgressState.Error);
            // ��δ��������쳣�����׳�
            throw;
        }
    }

    /// <summary>
    /// ��·����ȡ�ļ�
    /// </summary>
    /// <param name="path">�ļ�·��</param>
    private void ReadFromFile(string path)
    {
        using StreamReader reader = new(File.OpenRead(path));
        List = new(FileService.ReadFrom<TModel>(reader)
            .Select(i => TViewModel.FromModel(i.Key, i.Value)));

        SelectedItem = List.First();
    }


    /// <summary>
    /// д���ļ���·��
    /// </summary>
    /// <param name="path"></param>
    private void WriteToFile(string path)
    {
        // null���, ��ֹ����System.NullReferenceException
        if (List is null)
            return;

        // ����StreamWriter����д��, ʹ��using�ڲ�����Ҫʱ�Զ��ͷ��޷���GC���յ���Դ
        using StreamWriter writer = new(File.Create(path));
        // ��Listת����Map(Dictionary)�����SpawnerIO.WriteToд��
        FileService.WriteTo(List.ToDictionary(i => i.Name, i => i.ToModel()), writer);
    }
}