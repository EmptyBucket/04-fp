using System.Collections.Generic;
using System.Windows.Media;

namespace WordCloudMVVM.Model.Cloud.Build.Intersection
{
    public interface IIntersectionChecker
    {
        bool CheckIntersection(Geometry currentGeometry, IEnumerable<Geometry> geometryEnum);
    }
}
