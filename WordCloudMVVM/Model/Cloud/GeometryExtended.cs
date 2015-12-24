using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace WordCloudMVVM.Model.Cloud
{
    public static class GeometryExtended
    {
	    public static Geometry GetWordGeometry(WordStyle wordFontSize, Point point)
	    {
		    return GetFormattedText(wordFontSize).BuildGeometry(point);
	    }
            

	    private static FormattedText GetFormattedText(WordStyle wordFontSize)
	    {
            return new FormattedText(wordFontSize.Say, CultureInfo.InvariantCulture, FlowDirection.LeftToRight, new Typeface("Segoe UI"), wordFontSize.FontSize, Brushes.Black);
	    }

	    public static IEnumerable<Point> GetGeometryPoints(this Geometry geometry)
	    {
            return geometry.GetFlattenedPathGeometry().Figures
            .SelectMany(figure => figure.Segments)
            .SelectMany(segment => ((PolyLineSegment)segment).Points)
            .Select(poin => new Point((int)poin.X, (int)poin.Y));
	    }

        public static double GetGeometryWidth(this Geometry geometry)
        {
            IEnumerable<Point> pointGeometry = geometry.GetGeometryPoints();
            double max = pointGeometry.Max(point => point.X);
            double min = pointGeometry.Min(point => point.X);
            return max - min;
        }

        public static double GetGeometryHeight(this Geometry geometry)
        {
            IEnumerable<Point> pointGeometry = geometry.GetGeometryPoints();
            double max = pointGeometry.Max(point => point.Y);
            double min = pointGeometry.Min(point => point.Y);
            return max - min;
        }

	    public static double GetGeometryUp(this Geometry geometry)
	    {
		    return geometry.GetGeometryPoints().Max(point => point.Y);
	    }


	    public static double GetGeometryDown(this Geometry geometry)
	    {
		    return geometry.GetGeometryPoints().Min(point => point.Y);
	    }

	    public static double GetGeometryRight(this Geometry geometry)
	    {
		    return geometry.GetGeometryPoints().Max(point => point.X);
	    }

	    public static double GetGeometryLeft(this Geometry geometry)
	    {
		    return geometry.GetGeometryPoints().Min(point => point.X);
	    }

	    public static bool CheckIntersection(this Geometry currentGeometry, IEnumerable<Geometry> geometryEnum)
	    {
			return geometryEnum.Any(geometry => currentGeometry.FillContainsWithDetail(geometry) != IntersectionDetail.Empty);
	    }
            
    }
}
