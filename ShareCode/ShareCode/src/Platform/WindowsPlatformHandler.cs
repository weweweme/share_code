using CommunityToolkit.Maui.Storage;
using System.Collections.ObjectModel;

namespace ShareCode.src.Platform
{
    public class WindowsPlatformHandler : PlatformHandler
    {
        public override async Task OnOpenButtonClicked(ContentPage page, ObservableCollection<FileItem> originFileList, ObservableCollection<FileItem> uiFileList)
        {
            var folderResult = await FolderPicker.PickAsync(default);

            if (folderResult?.Folder != null)
            {
                var folderPath = folderResult.Folder.Path;

                string[] files = Directory.GetFiles(folderPath);
                uiFileList.Clear();
                originFileList.Clear();

                foreach (var file in files)
                {
                    var fileItem = new FileItem(Path.GetFileName(file), file);
                    fileItem.IsChecked = false;
                    originFileList.Add(fileItem);
                }

                FileFilter.FilterAndUpdateUI(LanguageManager.SelectedLanguage, originFileList, uiFileList);

                var exportButton = page.FindByName<Button>(GlobalConstants.EXPORT);
                exportButton.IsEnabled = uiFileList.Count > 0;

                if (LanguageManager.SelectedLanguage == ELanguage.None)
                {
                    exportButton.IsEnabled = false;
                }
            }
        }

        public override async Task OnExportButtonClicked(ContentPage page, ObservableCollection<FileItem> fileList)
        {
            if (!fileList.Any(file => file.IsChecked))
            {
                await page.DisplayAlert("경고", "내보낼 파일을 선택하세요.", "확인");
                return;
            }

            // 저장할 폴더 선택
            var folderResult = await FolderPicker.PickAsync(default);
            if (folderResult?.Folder == null)
            {
                await page.DisplayAlert("취소됨", "저장할 폴더를 선택하지 않았습니다.", "확인");
                return;
            }

            var saveFolderPath = folderResult.Folder.Path;

            // 저장할 파일 이름 입력 받기
            var fileName = await page.DisplayPromptAsync("파일 이름", "저장할 파일의 이름을 입력하세요:", "확인", "취소", "exported_file", maxLength: 50);
            if (string.IsNullOrWhiteSpace(fileName))
            {
                await page.DisplayAlert("취소됨", "파일 이름을 입력하지 않았습니다.", "확인");
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

                await page.DisplayAlert("성공", $"파일이 성공적으로 저장되었습니다.\n경로: {saveFilePath}", "확인");
                Console.WriteLine($"파일이 저장되었습니다: {saveFilePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"파일 저장 중 오류 발생: {ex.Message}");
                await page.DisplayAlert("오류", "파일 저장 중 오류가 발생했습니다.", "확인");
            }
        }

        private string GenerateHeader(string fileName)
        {
            const int DASH_TOTAL_LENGTH = 100;
            const int DIVISOR = 2;

            var fileBaseName = Path.GetFileNameWithoutExtension(fileName);
            var fileExtension = Path.GetExtension(fileName);

            var totalLength = fileName.Length;

            // 확장자가 가운데 오도록 계산
            var dashCount = DASH_TOTAL_LENGTH - totalLength;
            var leftDashCount = dashCount / DIVISOR;
            var rightDashCount = dashCount - leftDashCount;

            return $"{new string('-', leftDashCount)}{fileBaseName}{fileExtension}{new string('-', rightDashCount)}";
        }
    }
}
