namespace WordCloudMVVM.Model.Word
{
    public class WordFontSize
    {
        public string Say { get; }

        public int FontSize { get; }

        public WordFontSize(string word, int fontSize)
        {
            Say = word;
            FontSize = fontSize;
        }
    }
}
