using System.Text.RegularExpressions;

namespace WordCloudMVVM.Model
{
    public static class Cleaner
    {
        public static string Clean(string text)
        {
            const string punctuation = "’'\\[\\],(){}⟨⟩<>:‒…!.\\‐\\-?„“«»“”‘’‹›;1234567890_\\-+=\\/|@#$%^&*\"\r\n\t";
			var punctuationPattern = string.Format("[{0}]", punctuation);
            var punctuationReg = new Regex(punctuationPattern);

            const string lotSpacePattern = " {2,}";
            var lotSpaceReg = new Regex(lotSpacePattern);

            var cleanPunctuationText = punctuationReg.Replace(text, " ");
            var cleanText = lotSpaceReg.Replace(cleanPunctuationText, " ");

            return cleanText;
        }
    }
}
