namespace ShareCode.src
{
    public static class LanguageManager
    {
        private static readonly Dictionary<ELanguage, List<string>> _languageExtensions = new()
        {
            { ELanguage.CSharp, new List<string> { ".cs" } },
            { ELanguage.Java, new List<string> { ".java", ".class", ".jar" } },
            { ELanguage.C, new List<string> { ".c", ".h" } },
            { ELanguage.CPP, new List<string> { ".cpp", ".h", ".hpp" } },
            { ELanguage.Assembly, new List<string> { ".asm", ".s" } }
        };

        private static ELanguage _selectedLanguage = ELanguage.CSharp;
        public static ELanguage SelectedLanguage => _selectedLanguage;

        public static void SetLanguage(ELanguage language)
        {
            _selectedLanguage = language;
        }

        public static List<string> GetLanguage(ELanguage language)
        {
            return _languageExtensions[language];
        }
    }
}
