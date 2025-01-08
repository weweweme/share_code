using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using CommunityToolkit.Maui.Storage;

namespace ShareCode.src.Platform
{
    public class MacOSPlatformHandler : PlatformHandler
    {
        [SuppressMessage("Interoperability", "CA1416:플랫폼 호환성 유효성 검사")]
        public override async Task OnOpenButtonClicked(ContentPage page, ObservableCollection<FileItem> originFileList, ObservableCollection<FileItem> uiFileList)
        {
            try
            {
                // FolderPicker로 폴더 선택
                var folderResult = await FolderPicker.PickAsync(CancellationToken.None);

                // 폴더가 선택되지 않은 경우 처리
                if (folderResult.Folder == null)
                {
                    Console.WriteLine("폴더가 선택되지 않았습니다.");
                    return;
                }

                var folderPath = folderResult.Folder.Path;

                // 폴더 내부 파일 가져오기
                string[] files = Directory.GetFiles(folderPath);
                uiFileList.Clear();
                originFileList.Clear();

                foreach (var file in files)
                {
                    var fileItem = new FileItem(Path.GetFileName(file), file);
                    fileItem.IsChecked = false;
                    originFileList.Add(fileItem);
                }

                // UI 업데이트
                FileFilter.FilterAndUpdateUI(LanguageManager.SelectedLanguage, originFileList, uiFileList);

                // Export 버튼 활성화
                var exportButton = page.FindByName<Button>(GlobalConstants.EXPORT);
                exportButton.IsEnabled = uiFileList.Count > 0;

                if (LanguageManager.SelectedLanguage == ELanguage.None)
                {
                    exportButton.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"폴더 선택 중 오류 발생: {ex.Message}");
            }
        }
        
        public override async Task OnExportButtonClicked(ContentPage page, ObservableCollection<FileItem> fileList)
        {
        }
    }
}
