using System.Collections.Generic;
using System.Windows.Media;
using WordCloudMVVM.Model;
using WordCloudMVVM.Model.Cloud.Build.Intersection;
using WordCloudMVVM.Model.CloudPaint;
using WordCloudMVVM.Model.WordInspector;

namespace WordCloudMVVM.ViewModel
{
    public delegate string ReadDelegate(System.IO.Stream stream);
    public delegate DrawingImage DrawingGeometryDelegate(Dictionary<WordStyle, Geometry> geometryWords);
    public delegate Dictionary<WordStyle, Geometry> BuildGeometryDelegate(IEnumerable<WordStyle> words, int imageWidth, int imageHeight, int maxFont, CheckIntersectionDelegate IntersectionCheck);
    public delegate bool CheckIntersectionDelegate(Geometry currentGeometry, IEnumerable<Geometry> geometryEnum);
    public delegate IEnumerable<WordWeight> ParseDelegate(string text, CleanDelegate Clean, TokenizeDelegate Tokenize);
    public delegate IEnumerable<string> TokenizeDelegate(string text);
    public delegate string CleanDelegate(string text);
    public delegate bool IsBadDelegate(string word);

    public class ViewModelLocator
    {
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