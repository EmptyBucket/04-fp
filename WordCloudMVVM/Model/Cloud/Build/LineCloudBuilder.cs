﻿using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using WordCloudMVVM.Model.Cloud;
using WordCloudMVVM.Model.Cloud.Build.Intersection;

namespace WordCloudMVVM.Model.CloudPaint
{
    public class LineCloudBuilder : ICloudBuilder
    {
        private readonly IIntersectionChecker mIntersectionChecher;

        public LineCloudBuilder(IIntersectionChecker intersectionChecher)
        {
            mIntersectionChecher = intersectionChecher;
        }

        public Dictionary<WordStyle, Geometry> BuildWordsGeometry(IEnumerable<WordStyle> words, int imageWidth, int imageHeight, int maxFont)
        {
            var sortWords = words.OrderByDescending(word => word.FontSize);

            Dictionary<WordStyle, Geometry> wordGeometry = new Dictionary<WordStyle, Geometry>();

            List<Geometry> prewLineGeometry = new List<Geometry>();
            List<Geometry> currentLineGeometry = new List<Geometry>();

            IEnumerator<WordStyle> enumeratorWords = sortWords.GetEnumerator();
            for (double coordX = 0, coordY = 0; coordY < imageHeight && enumeratorWords.MoveNext();)
            {
                WordStyle word = enumeratorWords.Current;
                Geometry geometryWord = GeometryExtended.GetWordGeometry(word, new Point(coordX, coordY));

                for (double offset = 1; mIntersectionChecher.CheckIntersection(geometryWord, prewLineGeometry); offset++)
                    geometryWord = GeometryExtended.GetWordGeometry(word, new Point(coordX, coordY + offset));

                wordGeometry.Add(word, geometryWord);
                currentLineGeometry.Add(geometryWord);

                coordX = GeometryExtended.GetGeometryRight(geometryWord) + 1;
                if (coordX > imageWidth)
                {
                    coordX = 0;
                    coordY = currentLineGeometry.Max(geometry => GeometryExtended.GetGeometryDown(geometry));
                    prewLineGeometry = currentLineGeometry.ToList();
                    currentLineGeometry.Clear();
                }
            }

            return wordGeometry;
        }
    }
}