using System;
using System.Collections.Generic;
using System.Windows.Media;
using NHunspell;
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
    public delegate IEnumerable<WordWeight> ParseDelegate(IEnumerable<string> words);
    public delegate IEnumerable<string> StemTokenizeDelegate(string text, Hunspell hunspell);
    public delegate string CleanDelegate(string text);
    public delegate bool IsBadDelegate(string word, HashSet<string> badWords);

    public class ViewModelLocator
    {
        private static readonly string pathDicitonaryBadWord = System.IO.Path.Combine(Environment.CurrentDirectory, "InspectorDictionary", "InspectorDictionary.txt");

        private static readonly HashSet<string> mBadWords = new HashSet<string>(System.IO.File.ReadAllLines(pathDicitonaryBadWord));

        private static readonly string pathAffHunspell = System.IO.Path.Combine(Environment.CurrentDirectory, "HunspellDictionary", "ru_RU.aff");
        private static readonly string pathDicionaryHunspell = System.IO.Path.Combine(Environment.CurrentDirectory, "HunspellDictionary", "ru_RU.dic");

        private static readonly Hunspell mHunspell = new Hunspell(pathAffHunspell, pathDicionaryHunspell);

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MainViewModel Main => new MainViewModel(
            TextReader.Read,
            GeometryPainter.DrawGeometry,
            LineCloudBuilder.BuildWordsGeometry,
            IntersectionChecker.CheckIntersection,
            CountParser.CountParse,
            StemTokenizer.StemTokenize,
            mHunspell,
            Cleaner.Clean,
            BadWordInspector.IsBad,
            mBadWords);

        public static void Cleanup()
        {
        }
    }
}