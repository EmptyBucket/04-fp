using System.Collections.Generic;
using System.Windows.Media;

namespace WordCloudMVVM.Model
{
    static class GeometryPainter
    {
        public static DrawingImage DrawGeometry(Dictionary<WordStyle, Geometry> geomWordDict)
        {
            var visual = new DrawingVisual();
            using (DrawingContext drawingContext = visual.RenderOpen())
                foreach (var item in geomWordDict)
                    drawingContext.DrawGeometry(new SolidColorBrush(item.Key.Color), null, item.Value);
            return new DrawingImage(visual.Drawing);
        }
    }
}
