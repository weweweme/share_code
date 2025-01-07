using System.Collections.ObjectModel;

namespace ShareCode.src
{
    public static class FileFilter
    {
        // 언어에 맞는 파일을 필터링하고 UI 리스트를 업데이트하는 메서드
        public static void FilterAndUpdateUI(ELanguage language, IEnumerable<FileItem> originFileList, ObservableCollection<FileItem> uiFileList)
        {
            uiFileList.Clear();
            IEnumerable<FileItem> filesToAdd = language == ELanguage.None ? originFileList : originFileList.Where(file => LanguageManager.GetLanguage(language).Contains(Path.GetExtension(file.FilePath)));

            // 파일을 UI 리스트에 추가하고 체크 상태 초기화
            foreach (var file in filesToAdd)
            {
                file.IsChecked = false; // 기본적으로 체크 해제
                uiFileList.Add(file);
            }
        }
    }
}
