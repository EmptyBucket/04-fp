using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace WordCloudMVVM.Model.Cloud
{
    public static class GeometryExtended
    {
        public static Geometry GetWordGeometry(WordStyle wordFontSize, Point point) =>
            GetFormattedText(wordFontSize).BuildGeometry(point);

        private static FormattedText GetFormattedText(WordStyle wordFontSize) =>
            new FormattedText(wordFontSize.Say, CultureInfo.InvariantCulture, FlowDirection.LeftToRight, new Typeface("Segoe UI"), wordFontSize.FontSize, Brushes.Black);

        public static IEnumerable<Point> GetGeometryPoints(Geometry geometry) =>
            geometry.GetFlattenedPathGeometry().Figures
            .SelectMany(figure => figure.Segments)
            .SelectMany(segment => ((PolyLineSegment)segment).Points)
            .Select(poin => new Point((int)poin.X, (int)poin.Y));

        public static double GetGeometryWidth(Geometry geometry)
        {
            IEnumerable<Point> pointGeometry = GetGeometryPoints(geometry);
            double max = pointGeometry.Max(point => point.X);
            double min = pointGeometry.Min(point => point.X);
            return max - min;
        }

        public static double GetGeometryHeight(Geometry geometry)
        {
            IEnumerable<Point> pointGeometry = GetGeometryPoints(geometry);
            double max = pointGeometry.Max(point => point.Y);
            double min = pointGeometry.Min(point => point.Y);
            return max - min;
        }

        public static double GetGeometryUp(Geometry geometry) =>
            GetGeometryPoints(geometry).Max(point => point.Y);

        public static double GetGeometryDown(Geometry geometry) =>
            GetGeometryPoints(geometry).Min(point => point.Y);

        public static double GetGeometryRight(Geometry geometry) =>
            GetGeometryPoints(geometry).Max(point => point.X);

        public static double GetGeometryLeft(Geometry geometry) =>
            GetGeometryPoints(geometry).Min(point => point.X);
    }
}
