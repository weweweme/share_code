namespace ShareCode.src.System
{
    public class LanguageManager
    {
        // 언어별 확장자 매핑
        private readonly Dictionary<ELanguage, List<string>> languageExtensions = new()
        {
            { ELanguage.CSharp, new List<string> { ".cs" } },
            { ELanguage.Java, new List<string> { ".java", ".class", ".jar" } },
            { ELanguage.C, new List<string> { ".c", ".h" } },
            { ELanguage.CPP, new List<string> { ".cpp", ".h", ".hpp" } },
            { ELanguage.Assembly, new List<string> { ".asm", ".s" } }
        };

        // 현재 선택된 언어
        private ELanguage _selectedLanguage = ELanguage.CSharp; // 기본값
        public ELanguage SelectedLanguage => _selectedLanguage;
    }
}
