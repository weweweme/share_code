using CommunityToolkit.Maui.Storage;
using ShareCode.src.System;
using System.Collections.ObjectModel;

namespace ShareCode;

public partial class MainPage : ContentPage
{
    private readonly ObservableCollection<FileItem> originFileList = new(); // 원본 리스트
    private readonly ObservableCollection<FileItem> fileList = new(); // UI에 표시될 리스트

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

    private async void OnOpenButtonClicked(object? sender, EventArgs e)
    {
        if (OperatingSystem.IsWindows())
        {
            var folderResult = await FolderPicker.PickAsync(default);

            if (folderResult?.Folder != null)
            {
                var folderPath = folderResult.Folder.Path;

                string[] files = Directory.GetFiles(folderPath);
                fileList.Clear();

                foreach (var file in files)
                {
                    var fileItem = new FileItem(Path.GetFileName(file), file);
                    originFileList.Add(fileItem);
                    fileList.Add(fileItem);
                }

                var exportButton = this.FindByName<Button>("Export");
                exportButton.IsEnabled = fileList.Count > 0;

                Console.WriteLine($"선택된 폴더: {folderPath}");
            }
            else
            {
                Console.WriteLine("폴더 선택이 취소되었습니다.");
            }
        }
    }

    private void OnLanguageChanged(object? sender, CheckedChangedEventArgs e)
    {
        if (sender is RadioButton radioButton && e.Value)
        {
            // 라디오 버튼 Content를 ELanguage로 매핑
            LanguageManager.SetLanguage(radioButton.Content.ToString() switch
            {
                "C#" => ELanguage.CSharp,
                "Java" => ELanguage.Java,
                "C" => ELanguage.C,
                "C++" => ELanguage.CPP,
                "Assembly" => ELanguage.Assembly,
                _ => ELanguage.None
            });

            FilterFileList(LanguageManager.SelectedLanguage);
        }
    }

    private void FilterFileList(ELanguage language)
    {
        List<string> extensions = LanguageManager.GetLanguage(language);

        // 원본 리스트에서 필터링
        List<FileItem> filteredFiles = originFileList.Where(file => extensions.Contains(Path.GetExtension(file.FilePath))).ToList();

        // UI 리스트 업데이트
        fileList.Clear();
        foreach (var file in filteredFiles)
        {
            fileList.Add(file);
        }
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
