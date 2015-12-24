using System.Windows.Media;

namespace WordCloudMVVM.Model
{
    public class WordModelView
    {
        public Color Color { get; set; }

        public int FontSize { get; set; }

        public bool Active { get; set; }

	    private string say;
        public string Say { get { return say; } }

        public WordModelView(string word, int fontSize, Color color, bool active)
        {
            say = word;
            FontSize = fontSize;
            Color = color;
            Active = active;
        }
    }
}
