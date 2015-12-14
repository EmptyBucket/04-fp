using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace WordCloudMVVM.Model
{
    public class CloudWavePainter : CloudPainter
    {
        protected override IEnumerable<WordPosition> GetPositionWord(IEnumerable<WordFontSize> words, int imageSizeX, int imageSizeY)
        {
            Point centreBitmap = new Point(imageSizeX / 2, imageSizeY / 2);
            WordPosition firstPositionWord = new WordPosition(words.ElementAt(0), centreBitmap);
            Geometry geometryFirstWord = GetWordGeometry(firstPositionWord);

            List<WordFontSize> wordList = new List<WordFontSize>(words.Skip(1));

            List<WordPosition> positionWords = new List<WordPosition> { firstPositionWord };
            List<Geometry> geometryList = new List<Geometry>() { geometryFirstWord };
            IEnumerable<Point> geometryPointFirtWord = GetGeometryPoints(geometryFirstWord);
            List<Point> busyPoint = new List<Point>(geometryPointFirtWord);
            List<Point> lastBusy = new List<Point>(geometryPointFirtWord);

            while (wordList.Count != 0)
            {
                var freeVicinty = lastBusy
                    .SelectMany(point => GetVicinity(point))
                    .Where(vicinty => !busyPoint.Contains(vicinty))
                    .ToArray();


                lastBusy.Clear();
                foreach (var freeNeighbor in freeVicinty)
                    foreach (var word in wordList.ToArray())
                    {
                        WordPosition positionWord = new WordPosition(word, freeNeighbor);
                        Geometry geometryWord = GetWordGeometry(positionWord);
                        if (!CheckIntersection(geometryWord, geometryList))
                        {
                            wordList.Remove(word);
                            geometryList.Add(geometryWord);

                            IEnumerable<Point> geometryPoint = GetGeometryPoints(geometryWord);
                            busyPoint.AddRange(geometryPoint);
                            lastBusy.AddRange(geometryPoint);

                            positionWords.Add(positionWord);
                            break;
                        }
                    }

                lastBusy.AddRange(freeVicinty);
                busyPoint.AddRange(freeVicinty);
            }

            return positionWords;
        }

        private IEnumerable<Point> GetVicinity(Point point) => GetVicinity(point, 1);

        private Point[] GetVicinity(Point currentPoint, int value) => new Point[]
        {
            new Point(currentPoint.X + value, currentPoint.Y),
            new Point(currentPoint.X + value, currentPoint.Y + 1),
            new Point(currentPoint.X, currentPoint.Y + value),
            new Point(currentPoint.X - value, currentPoint.Y + value),
            new Point(currentPoint.X - value, currentPoint.Y),
            new Point(currentPoint.X - value, currentPoint.Y - value),
            new Point(currentPoint.X, currentPoint.Y - value),
            new Point(currentPoint.X + value, currentPoint.Y - value)
        };
    }
}
