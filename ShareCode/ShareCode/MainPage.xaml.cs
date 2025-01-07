using ShareCode.src;
using ShareCode.src.Platform;
using System.Collections.ObjectModel;

namespace ShareCode;

public partial class MainPage : ContentPage
{
    private readonly ObservableCollection<FileItem> originFileList = new(); // 원본 리스트
    private readonly ObservableCollection<FileItem> uiFileList = new(); // UI에 표시될 리스트

    public MainPage()
    {
        InitializeComponent();

        // 플랫폼 핸들러 생성
        var platformHandler = PlatformHandlerFactory.GetPlatformHandler();

        // 파일 목록 바인딩
        FileListView.ItemsSource = uiFileList;

        // 버튼 클릭 이벤트 핸들러 설정
        var openButton = this.FindByName<Button>(GlobalConstants.OPEN);
        var exportButton = this.FindByName<Button>(GlobalConstants.EXPORT);

        openButton.Clicked += async (s, e) => await platformHandler.OnOpenButtonClicked(this, originFileList, uiFileList);
        exportButton.Clicked += async (s, e) => await platformHandler.OnExportButtonClicked(this, uiFileList);

        // Export 버튼은 초기에는 비활성화 상태
        exportButton.IsEnabled = false;
    }

    private void OnLanguageChanged(object? sender, CheckedChangedEventArgs e)
    {
        if (sender is RadioButton radioButton && e.Value)
        {
            LanguageManager.SetLanguage(radioButton.Content.ToString()!);
            FileFilter.FilterAndUpdateUI(LanguageManager.SelectedLanguage, originFileList, uiFileList);
        }
    }
}
