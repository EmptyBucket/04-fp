using System.Windows.Media;
using WordCloudMVVM.Model.Word;

namespace WordCloudMVVM.Model
{
    public class WordStyle : WordFontSize
    {
        public Color Color { get; private set; }

        public WordStyle(string word, int fontSize, Color color) : base(word, fontSize)
        {
            Color = color;
        }
    }
}
