using System.Text.RegularExpressions;

namespace WordCloudMVVM.Model
{
    public static class Cleaner
    {
        public static string Clean(string text)
        {
            string punctuation = "’'\\[\\],(){}⟨⟩<>:‒…!.\\‐\\-?„“«»“”‘’‹›;1234567890_\\-+=\\/|@#$%^&*\"\r\n\t";
            string punctuationPattern = $"[{punctuation}]";
            Regex punctuationReg = new Regex(punctuationPattern);

            string lotSpacePattern = " {2,}";
            Regex lotSpaceReg = new Regex(lotSpacePattern);

            string cleanPunctuationText = punctuationReg.Replace(text, " ");
            string cleanText = lotSpaceReg.Replace(cleanPunctuationText, " ");

            return cleanText;
        }
    }
}
