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

        private static readonly Dictionary<string, ELanguage> _languageMapping = new()
        {
            { GlobalConstants.CSHARP, ELanguage.CSharp },
            { GlobalConstants.JAVA, ELanguage.Java },
            { GlobalConstants.C, ELanguage.C },
            { GlobalConstants.CPP, ELanguage.CPP },
            { GlobalConstants.ASSEMBLY, ELanguage.Assembly }
        };

        private static ELanguage _selectedLanguage = ELanguage.CSharp;
        public static ELanguage SelectedLanguage => _selectedLanguage;

        public static void SetLanguage(string languageName)
        {
            _selectedLanguage = _languageMapping.TryGetValue(languageName, out var language)
                ? language
                : ELanguage.None;
        }

        public static List<string> GetLanguage(ELanguage language)
        {
            System.Diagnostics.Debug.Assert(_languageExtensions.ContainsKey(language), $"Invalid language: {language}");

            if (language == ELanguage.None)
            {
                // "All Files" 선택 시 모든 파일을 포함하도록 처리
                return new List<string> { "*" }; // '*'는 모든 파일을 의미
            }

            if (_languageExtensions.TryGetValue(language, out var extensions))
            {
                return extensions;
            }

            // 기본값 반환: 빈 확장자 리스트가 아닌 "알 수 없는 언어"용 기본 확장자 리스트 제공
            return new List<string> { ".txt" };
        }
    }
}