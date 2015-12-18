using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Media;
using NHunspell;
using WordCloudMVVM.Model;
using WordCloudMVVM.Model.Cloud.Build.Intersection;
using WordCloudMVVM.Model.CloudPaint;

namespace WordCloudMVVM.ViewModel
{
    public delegate DrawingImage DrawGeometryWordsDelegate(IEnumerable<WordStyle> words, int imageWidth, int imageHeight, int maxFont);
    public delegate bool CheckIntersectionDelegate(Geometry currentGeometry, IEnumerable<Geometry> geometryEnum);
    public delegate InspectWords ParseDelegate(string path);

    public class ViewModelLocator
    {
        private static InspectWords Parse(string pathFile)
        {
            string pathAffHunspell = Path.Combine(Environment.CurrentDirectory, "HunspellDictionary", "ru_RU.aff");
            string pathDicionaryHunspell = Path.Combine(Environment.CurrentDirectory, "HunspellDictionary", "ru_RU.dic");

            var hunspell = new Hunspell(pathAffHunspell, pathDicionaryHunspell);

            using (FileStream fileStream = new FileStream(pathFile, FileMode.Open))
            {
                string text = Model.TextReader.Read(fileStream);
                string cleanText = Cleaner.Clean(text);
                var words = StemTokenizer.StemTokenize(cleanText, hunspell);
                var wordsWeight = CountParser.CountParse(words);
                var inspectWords = BadWordInspect(wordsWeight);

                return inspectWords;
            }
        }

        private static InspectWords BadWordInspect(HashSet<WordWeight> words)
        {
            string pathDicitonaryBadWord = Path.Combine(Environment.CurrentDirectory, "InspectorDictionary", "InspectorDictionary.txt");

            var badWordsDict = new HashSet<string>(File.ReadAllLines(pathDicitonaryBadWord));

            var goodWords = words.Where(wordWeight => !BadWordInspector.IsBad(wordWeight.Say, badWordsDict));
            var badWords = words.Where(wordWeight => BadWordInspector.IsBad(wordWeight.Say, badWordsDict));

            return new InspectWords(goodWords, badWords);
        }

        private static DrawingImage DrawGeometryWords(IEnumerable<WordStyle> words, int imageWidth, int imageHeight, int maxFont)
        {
            var wordsGeometry = LineCloudBuilder.BuildWordsGeometry(words, imageWidth, imageHeight, maxFont, IntersectionChecker.CheckIntersection);
            return GeometryPainter.DrawGeometry(wordsGeometry);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MainViewModel Main => new MainViewModel(DrawGeometryWords, Parse);

        public static void Cleanup()
        {
        }
    }
}