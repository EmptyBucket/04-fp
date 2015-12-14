using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace WordCloudMVVM.Model.WordParser
{
    public class CloudRandomPainter : CloudPainter
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

            Random rand = new Random();
            while (wordList.Count != 0)
            {
                var freeVicinty =
                    Enumerable.Range(0, 100)
                    .Select(num => new Point(rand.Next(0, imageSizeX), rand.Next(0, imageSizeY)))
                    .Where(vicinty => !busyPoint.Contains(vicinty));


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

                            positionWords.Add(positionWord);
                            break;
                        }
                    }

                busyPoint.AddRange(freeVicinty);
            }

            return positionWords;
        }
    }
}
