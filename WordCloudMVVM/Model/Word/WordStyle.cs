using System.Windows.Media;

namespace WordCloudMVVM.Model
{
    public class WordStyle
    {
	    private string say;
        public string Say { get { return say; } }

	    private int fontSize;
        public int FontSize { get { return fontSize; } }

	    private Color color;
        public Color Color { get { return color; } }

        public WordStyle(string word, int fontSize, Color color) : this(word, fontSize)
        {
            this.color = color;
        }

        public WordStyle(string word, int fontSize)
        {
            say = word;
            this.fontSize = fontSize;
            color = Colors.Black;
        }
    }
}
