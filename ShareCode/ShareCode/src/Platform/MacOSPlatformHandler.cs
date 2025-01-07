using System.Collections.ObjectModel;

namespace ShareCode.src.Platform
{
    public class MacOSPlatformHandler : PlatformHandler
    {
        public override Task OnOpenButtonClicked(ContentPage page, ObservableCollection<FileItem> originFileList, ObservableCollection<FileItem> fileList)
        {
            throw new NotImplementedException();
        }

        public override Task OnExportButtonClicked(ContentPage page, ObservableCollection<FileItem> fileList)
        {
            throw new NotImplementedException();
        }
    }
}
