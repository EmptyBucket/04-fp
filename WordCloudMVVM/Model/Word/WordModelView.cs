using System.Windows.Media;

namespace WordCloudMVVM.Model.Word
{
    public class WordModelView
    {
        public Color Color { get; set; }

        public int FontSize { get; set; }

        public bool Active { get; set; }

        private readonly string _say;
        public string Say { get { return _say; } }

        public WordModelView(string word, int fontSize, Color color, bool active)
        {
            _say = word;
            FontSize = fontSize;
            Color = color;
            Active = active;
        }
    }
}
