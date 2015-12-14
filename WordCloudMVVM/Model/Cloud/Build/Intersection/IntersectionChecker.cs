using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace WordCloudMVVM.Model.Cloud.Build.Intersection
{
    public class IntersectionChecker : IIntersectionChecker
    {
        public bool CheckIntersection(Geometry currentGeometry, IEnumerable<Geometry> geometryEnum) =>
            geometryEnum.Any(geometry => currentGeometry.FillContainsWithDetail(geometry) != IntersectionDetail.Empty);
    }
}
