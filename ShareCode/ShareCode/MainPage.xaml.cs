namespace ShareCode;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();

        // 버튼 클릭 이벤트 핸들러 설정
        var openButton = this.FindByName<Button>("Open");
        var exportButton = this.FindByName<Button>("Export");

        openButton.Clicked += OnOpenButtonClicked;
        exportButton.Clicked += OnExportButtonClicked;

        // Export 버튼은 초기에는 비활성화 상태
        exportButton.IsEnabled = false;
    }

    // Open 버튼 클릭 이벤트 핸들러
    private void OnOpenButtonClicked(object? sender, EventArgs e)
    {
        if (sender == null)
        { 
            // TODO: 추가 처리
            return; 
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
