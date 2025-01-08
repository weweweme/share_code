﻿using ShareCode.src;
using ShareCode.src.Platform;
using System.Collections.ObjectModel;
using UIKit;

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

        // Mac Catalyst 전용 창 설정
#if MACCATALYST
        ConfigureMacCatalystWindow();
#else
        // 다른 플랫폼 (예: Windows)
        var window = Application.Current!.Windows[0];
        window.Height = 800; // 원하는 높이로 설정
        window.Width = 600;  // 원하는 너비로 설정
        window.MinimumHeight = 800; // 최소 높이 고정
        window.MinimumWidth = 600;  // 최소 너비 고정
#endif
    }
    
    private void ConfigureMacCatalystWindow()
    {
        // if (UIApplication.SharedApplication.ConnectedScenes.FirstOrDefault<object>() is UIWindowScene windowScene)
        // {
        //     // 창 크기와 최소/최대 크기 설정
        //     windowScene.SizeRestrictions.MinimumSize = new CoreGraphics.CGSize(600, 800); // 최소 크기
        //     windowScene.SizeRestrictions.MaximumSize = new CoreGraphics.CGSize(600, 800); // 최대 크기
        // }
    }

    private void OnLanguageChanged(object? sender, CheckedChangedEventArgs e)
    {
        if (sender is RadioButton radioButton && e.Value)
        {
            LanguageManager.SetLanguage(radioButton.Content.ToString()!);
            FileFilter.FilterAndUpdateUI(LanguageManager.SelectedLanguage, originFileList, uiFileList);

            var exportButton = this.FindByName<Button>(GlobalConstants.EXPORT);
            if (LanguageManager.SelectedLanguage == ELanguage.None)
            {
                exportButton.IsEnabled = false;
            }
            else
            {
                exportButton.IsEnabled = true;
            }
        }
    }
}
