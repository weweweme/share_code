namespace ShareCode.src.System
{
    public class LanguageManager
    {
        private readonly Dictionary<ELanguage, List<string>> _languageExtensions = new()
        {
            { ELanguage.CSharp, new List<string> { ".cs" } },
            { ELanguage.Java, new List<string> { ".java", ".class", ".jar" } },
            { ELanguage.C, new List<string> { ".c", ".h" } },
            { ELanguage.CPP, new List<string> { ".cpp", ".h", ".hpp" } },
            { ELanguage.Assembly, new List<string> { ".asm", ".s" } }
        };
        public IReadOnlyDictionary<ELanguage, List<string>> LanguageExtensions => _languageExtensions;

        private ELanguage _selectedLanguage = ELanguage.CSharp;
        public ELanguage SelectedLanguage => _selectedLanguage;
    }
}
