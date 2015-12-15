using WordCloudMVVM.Model;
using WordCloudMVVM.Model.Cloud.Build.Intersection;
using WordCloudMVVM.Model.CloudPaint;
using WordCloudMVVM.Model.WordInspector;

namespace WordCloudMVVM.ViewModel
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MainViewModel Main => new MainViewModel(
            TextReader.Read,
            GeometryPainter.DrawGeometry,
            LineCloudBuilder.BuildWordsGeometry,
            IntersectionChecker.CheckIntersection,
            Parser.Parse,
            StemTokenizer.Tokenize,
            Cleaner.Clean,
            BadWordInspector.IsBad);

        public static void Cleanup()
        {
        }
    }
}