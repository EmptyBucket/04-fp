using System.Windows.Media;

namespace WordCloudMVVM.Model
{
    public class WordStyle
    {
        public string Say { get; }

        public int FontSize { get; }

        public Color Color { get; }

        public WordStyle(string word, int fontSize, Color color) : this(word, fontSize)
        {
            Color = color;
        }

        public WordStyle(string word, int fontSize)
        {
            Say = word;
            FontSize = fontSize;
            Color = Colors.Black;
        }
    }
}
