using CommunityToolkit.Maui.Storage;
using System.Collections.ObjectModel;

namespace ShareCode;

public partial class MainPage : ContentPage
{
    private ObservableCollection<string> fileList = new();

    public MainPage()
    {
        InitializeComponent();

        // 파일 목록 바인딩
        FileListView.ItemsSource = fileList;

        // 버튼 클릭 이벤트 핸들러 설정
        var openButton = this.FindByName<Button>("Open");
        var exportButton = this.FindByName<Button>("Export");

        openButton.Clicked += OnOpenButtonClicked;
        exportButton.Clicked += OnExportButtonClicked;

        // Export 버튼은 초기에는 비활성화 상태
        exportButton.IsEnabled = false;
    }

    // Open 버튼 클릭 이벤트 핸들러
    private async void OnOpenButtonClicked(object? sender, EventArgs e)
    {
        if (sender == null)
        { 
            // TODO: 추가 처리
            return; 
        }

        if (OperatingSystem.IsWindows())
        {
            FolderPickerResult folder = await FolderPicker.PickAsync(default);

            if (folder != null)
            {
                var folderPath = folder.Folder!.Path;

                // 파일 목록을 가져와 ObservableCollection에 추가
                string[] files = Directory.GetFiles(folderPath);
                fileList.Clear();
                foreach (var file in files)
                {
                    fileList.Add(Path.GetFileName(file)); // 파일명만 추가
                }

                var exportButton = this.FindByName<Button>("Export");
                exportButton.IsEnabled = fileList.Count > 0;

                Console.WriteLine($"선택된 폴더: {folderPath}");
            }
        }

        // TODO: Open 버튼 클릭 동작 구현 (폴더 선택 등)
        Console.WriteLine("Open 버튼 클릭");
    }

    // Export 버튼 클릭 이벤트 핸들러
    private void OnExportButtonClicked(object? sender, EventArgs e)
    {
        if (sender == null)
        {
            // TODO: 추가 처리
            return;
        }

        // TODO: Export 버튼 클릭 동작 구현 (파일 내보내기 등)
        Console.WriteLine("Export 버튼 클릭");
    }
}
