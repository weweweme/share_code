namespace ShareCode.src.System
{
    public class FileItem
    {
        private readonly string _fileName;
        private readonly string _filePath;
        private bool _isChecked;

        public FileItem(string fileName, string filePath)
        {
            _fileName = fileName ?? throw new ArgumentNullException(nameof(fileName), "FileName은 null일 수 없습니다.");
            _filePath = filePath ?? throw new ArgumentNullException(nameof(filePath), "FilePath는 null일 수 없습니다.");
            _isChecked = false;
        }

        public string FileName => _fileName;
        public string FilePath => _filePath;
        public bool IsChecked
        {
            get => _isChecked;
            set => _isChecked = value;
        }
    }
}
