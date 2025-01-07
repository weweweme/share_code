using System.Collections.ObjectModel;

namespace ShareCode.src.Platform
{
    public class MacOSPlatformHandler : PlatformHandler
    {
        public override async Task OnOpenButtonClicked(ContentPage page, ObservableCollection<FileItem> originFileList, ObservableCollection<FileItem> uiFileList)
        {
            // macOS에서는 FolderPicker 대신 FilePicker 사용
            var folderResult = await FilePicker.PickAsync(new PickOptions
            {
                PickerTitle = "폴더를 선택하세요."
            });

            if (folderResult != null)
            {
                var folderPath = folderResult.FullPath;
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
            throw new NotImplementedException();
        }
    }
}
