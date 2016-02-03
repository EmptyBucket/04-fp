using System.Windows.Media;

namespace WordCloudMVVM.Model.Word
{
    public class WordStyle
    {
        private readonly string _say;
        public string Say { get { return _say; } }

        private readonly int _fontSize;
        public int FontSize { get { return _fontSize; } }

        private readonly Color _color;
        public Color Color { get { return _color; } }

        public WordStyle(string word, int fontSize, Color color) : this(word, fontSize)
        {
            _color = color;
        }

        public WordStyle(string word, int fontSize)
        {
            _say = word;
            _fontSize = fontSize;
            _color = Colors.Black;
        }
    }
}
