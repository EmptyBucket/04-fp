using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NHunspell;

namespace WordCloudMVVM.Model
{
    static class StemTokenizer
    {
        private static readonly string pathAffHunspell = Path.Combine(Environment.CurrentDirectory, "HunspellDictionary", "ru_RU.aff");
        private static readonly string pathDicionaryHunspell = Path.Combine(Environment.CurrentDirectory, "HunspellDictionary", "ru_RU.dic");

        private static readonly Hunspell mHunspell = new Hunspell(pathAffHunspell, pathDicionaryHunspell);

        public static IEnumerable<string> Tokenize(string text) =>
            text
            .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        public static IEnumerable<string> StemTokenize(string text) =>
            Tokenize(text)
            .Select(word => mHunspell.Stem(word).FirstOrDefault() ?? word);
    }
}
