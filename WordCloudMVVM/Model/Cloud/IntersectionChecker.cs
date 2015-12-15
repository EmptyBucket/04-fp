using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace WordCloudMVVM.Model.Cloud.Build.Intersection
{
    static class IntersectionChecker
    {
        public static bool CheckIntersection(Geometry currentGeometry, IEnumerable<Geometry> geometryEnum) =>
            geometryEnum.Any(geometry => currentGeometry.FillContainsWithDetail(geometry) != IntersectionDetail.Empty);
    }
}
