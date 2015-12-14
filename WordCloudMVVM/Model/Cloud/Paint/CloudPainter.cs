using System.Collections.Generic;
using System.Windows.Media;
using WordCloudMVVM.Model.CloudPaint;

namespace WordCloudMVVM.Model
{
    public class CloudPainter : ICloudPainter
    {
        private readonly ICloudBuilder mCloudPainter;

        public CloudPainter(ICloudBuilder clouPainter)
        {
            mCloudPainter = clouPainter;
        }

        protected DrawingImage DrawingImage(Dictionary<WordStyle, Geometry> geomWordDict)
        {
            var visual = new DrawingVisual();
            using (DrawingContext drawingContext = visual.RenderOpen())
                foreach (var item in geomWordDict)
                    drawingContext.DrawGeometry(new SolidColorBrush(item.Key.Color), null, item.Value);
            return new DrawingImage(visual.Drawing);
        }

        public DrawingImage DrawCloudWord(IEnumerable<WordStyle> words, int imageWidht, int imageHeight, int maxFont) =>
            DrawingImage(mCloudPainter.BuildWordsGeometry(words, imageWidht, imageHeight, maxFont));
    }
}
