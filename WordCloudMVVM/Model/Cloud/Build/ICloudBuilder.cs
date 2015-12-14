using System.Collections.Generic;
using System.Windows.Media;

namespace WordCloudMVVM.Model.CloudPaint
{
    public interface ICloudBuilder
    {
        Dictionary<WordStyle, Geometry> BuildWordsGeometry(IEnumerable<WordStyle> words, int imageWidth, int imageHeight, int maxFont);
    }
}
