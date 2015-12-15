using System;
using System.Collections.Generic;
using System.IO;

namespace WordCloudMVVM.Model.WordInspector
{
    static class BadWordInspector
    {
        private static readonly string pathDicitonaryBadWord = Path.Combine(Environment.CurrentDirectory, "InspectorDictionary", "InspectorDictionary.txt");

        private static readonly HashSet<string> mBadWords = new HashSet<string>(File.ReadAllLines(pathDicitonaryBadWord));

        public static bool IsBad(string word) =>
            mBadWords.Contains(word);
    }
}
