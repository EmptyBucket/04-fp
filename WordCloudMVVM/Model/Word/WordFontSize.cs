namespace WordCloudMVVM.Model.Word
{
    public class WordFontSize : Word
    {
        public int FontSize { get; private set; }

        public WordFontSize(string word, int fontSize) : base(word)
        {
            FontSize = fontSize;
        }
    }
}
