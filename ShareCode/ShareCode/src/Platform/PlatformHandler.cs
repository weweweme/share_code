using System.Collections.ObjectModel;

namespace ShareCode.src.Platform
{
    public abstract class PlatformHandler
    {
        public abstract Task OnOpenButtonClicked(ContentPage page, ObservableCollection<FileItem> originFileList, ObservableCollection<FileItem> fileList);
        public abstract Task OnExportButtonClicked(ContentPage page, ObservableCollection<FileItem> fileList);
    }
}
