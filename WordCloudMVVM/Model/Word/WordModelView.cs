using System.Windows.Media;

namespace WordCloudMVVM.Model
{
    public class WordModelView
    {
        public Color Color { get; set; }

        public int FontSize { get; set; }

        public bool Active { get; set; }

        public string Say { get; set; }

        public WordModelView(string word, int fontSize, Color color, bool active)
        {
            Say = word;
            FontSize = fontSize;
            Color = color;
            Active = active;
        }
    }
}
