using System.Collections.Generic;
using System.Windows.Media;

namespace WordCloudMVVM.Model
{
    public interface ICloudPainter
    {
        DrawingImage DrawCloudWord(IEnumerable<WordStyle> words, int imageWidht, int imageHeight, int maxFont);
    }
}
