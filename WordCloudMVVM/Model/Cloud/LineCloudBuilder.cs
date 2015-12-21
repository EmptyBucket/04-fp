using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using WordCloudMVVM.Model.Cloud;

namespace WordCloudMVVM.Model.CloudPaint
{
    static class LineCloudBuilder
    {
        public static Dictionary<WordStyle, Geometry> BuildWordsGeometry(IEnumerable<WordStyle> words, int imageWidth, int imageHeight, int maxFont)
        {
            var sortWords = words.OrderByDescending(word => word.FontSize);

            Dictionary<WordStyle, Geometry> geometryWords = new Dictionary<WordStyle, Geometry>();

            List<Geometry> prewLineGeometry = new List<Geometry>();
            List<Geometry> currentLineGeometry = new List<Geometry>();

            IEnumerator<WordStyle> enumeratorWords = sortWords.GetEnumerator();
            for (double coordY = 0; coordY < imageHeight; coordY = currentLineGeometry.Max(geometry => geometry.GetGeometryDown()))
            {
                prewLineGeometry = currentLineGeometry.ToList();
                currentLineGeometry.Clear();
                for(double coordX = 0; coordX < imageWidth && enumeratorWords.MoveNext();)
                {
                    WordStyle word = enumeratorWords.Current;
                    Geometry geometryWord = GeometryExtended.GetWordGeometry(word, new Point(coordX, coordY));

                    for (double offset = 1; geometryWord.CheckIntersection(prewLineGeometry); offset++)
                        geometryWord = GeometryExtended.GetWordGeometry(word, new Point(coordX, coordY + offset));

                    geometryWords.Add(word, geometryWord);
                    currentLineGeometry.Add(geometryWord);
                    coordX = geometryWord.GetGeometryRight() + 1;
                }
            }

            return geometryWords;
        }
    }
}
