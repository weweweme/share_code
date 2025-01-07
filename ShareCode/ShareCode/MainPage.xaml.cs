using CommunityToolkit.Maui.Storage;
using ShareCode.src;
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

    private async void OnExportButtonClicked(object? sender, EventArgs e)
    {
        if (!fileList.Any(file => file.IsChecked))
        {
            await DisplayAlert("경고", "내보낼 파일을 선택하세요.", "확인");
            return;
        }

        // 저장할 폴더 선택
        var folderResult = await FolderPicker.PickAsync(default);
        if (folderResult?.Folder == null)
        {
            await DisplayAlert("취소됨", "저장할 폴더를 선택하지 않았습니다.", "확인");
            return;
        }

        var saveFolderPath = folderResult.Folder.Path;

        // 저장할 파일 이름 입력 받기
        var fileName = await DisplayPromptAsync("파일 이름", "저장할 파일의 이름을 입력하세요:", "확인", "취소", "exported_file", maxLength: 50);
        if (string.IsNullOrWhiteSpace(fileName))
        {
            await DisplayAlert("취소됨", "파일 이름을 입력하지 않았습니다.", "확인");
            return;
        }

        // 파일 경로 설정
        var saveFilePath = Path.Combine(saveFolderPath, $"{fileName}.txt");

        // 체크된 파일 필터링
        var selectedFiles = fileList.Where(file => file.IsChecked).ToList();

        try
        {
            using (var writer = new StreamWriter(saveFilePath, false))
            {
                foreach (var file in selectedFiles)
                {
                    // 파일명과 확장자를 기반으로 헤더 생성
                    string header = GenerateHeader(file.FileName);
                    string footer = new string('-', 100);

                    writer.WriteLine(header);
                    writer.WriteLine();
                    writer.WriteLine();
                    writer.WriteLine(await File.ReadAllTextAsync(file.FilePath));
                    writer.WriteLine();
                    writer.WriteLine();
                    writer.WriteLine(footer);
                    writer.WriteLine();
                    writer.WriteLine();
                    writer.WriteLine();
                    writer.WriteLine();
                }
            }

            await DisplayAlert("성공", $"파일이 성공적으로 저장되었습니다.\n경로: {saveFilePath}", "확인");
            Console.WriteLine($"파일이 저장되었습니다: {saveFilePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"파일 저장 중 오류 발생: {ex.Message}");
            await DisplayAlert("오류", "파일 저장 중 오류가 발생했습니다.", "확인");
        }
    }

    // 헤더 및 푸터 생성 메서드
    private string GenerateHeader(string fileName)
    {
        // 파일명과 확장자 구분
        var fileBaseName = Path.GetFileNameWithoutExtension(fileName);
        var fileExtension = Path.GetExtension(fileName);

        // 파일명+확장자의 길이
        var totalLength = fileName.Length;

        // 확장자가 가운데 오도록 계산
        var dashCount = 100 - totalLength;
        var leftDashCount = dashCount / 2;
        var rightDashCount = dashCount - leftDashCount;

        // 결과 문자열 생성
        return $"{new string('-', leftDashCount)}{fileBaseName}{fileExtension}{new string('-', rightDashCount)}";
    }

}
