
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Media;
using NHunspell;
using WordCloudMVVM.Model;
using WordCloudMVVM.Model.Cloud;
using WordCloudMVVM.Model.Word;

namespace WordCloudMVVM.ViewModel
{
    public delegate DrawingImage DrawGeometryWordsDelegate(IReadOnlyCollection<WordStyle> words, int imageWidth, int imageHeight, int maxFont);

    public delegate string GetTextFromFileDelegate(string filePath);

    public delegate HashSet<WordWeight> GetParsedWordsDelegate(string text);

    public delegate InspectWords BadWordInspectDelegate(HashSet<WordWeight> parsedWords);

    public class ViewModelLocator
    {
        private static string GetTextFromFile(string filePath)
        {
            using (var fileStream = new FileStream(filePath, FileMode.Open))
            {
                var text = Model.TextReader.Read(fileStream, Encoding.ASCII);
                return text;
            }
        }

        private static IReadOnlyCollection<string> GetWords(string text)
        {
            var pathAffHunspell = Path.Combine(Environment.CurrentDirectory, "HunspellDictionary", "ru_RU.aff");
            var pathDicionaryHunspell = Path.Combine(Environment.CurrentDirectory, "HunspellDictionary", "ru_RU.dic");

            var hunspell = new Hunspell(pathAffHunspell, pathDicionaryHunspell);

            var cleanText = Cleaner.Clean(text);
            var words = StemTokenizer.StemTokenize(cleanText, hunspell);
            return words;
        }

        private static HashSet<WordWeight> GetParsedWords(string text)
        {
            var words = GetWords(text);
            var wordsWeight = CountWordParser.Parse(words);
            return wordsWeight;
        }

        private static InspectWords BadWordInspect(HashSet<WordWeight> words)
        {
            var pathDicitonaryBadWord = Path.Combine(Environment.CurrentDirectory, "InspectorDictionary", "InspectorDictionary.txt");

            var badWordsDict = new HashSet<string>(File.ReadAllLines(pathDicitonaryBadWord));

            var goodWords = words.Where(wordWeight => !BadWordInspector.IsBad(wordWeight.Say, badWordsDict)).ToArray();
            var badWords = words.Where(wordWeight => BadWordInspector.IsBad(wordWeight.Say, badWordsDict)).ToArray();

            return new InspectWords(goodWords, badWords);
        }

        private static DrawingImage DrawGeometryWords(IReadOnlyCollection<WordStyle> words, int imageWidth, int imageHeight, int maxFont)
        {
            var wordsGeometry = LineCloudBuilder.BuildWordsGeometry(words, imageWidth, imageHeight, maxFont);
            var paintedGeometryWords = GeometryPainter.DrawGeometry(wordsGeometry);
            return paintedGeometryWords;
        }

	    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
		    "CA1822:MarkMembersAsStatic",
		    Justification = "This non-static member is needed for data binding purposes.")]
	    public MainViewModel Main {
            get
            {
                return new MainViewModel(
                    DrawGeometryWords,
                    GetTextFromFile, 
                    GetParsedWords, 
                    BadWordInspect);
            }
        }

        public static void Cleanup()
        {
        }
    }
}