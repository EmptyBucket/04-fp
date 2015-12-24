using System;
using System.Linq;
using NHunspell;

namespace WordCloudMVVM.Model
{
    public static class StemTokenizer
    {
        public static string[] Tokenize(string text) =>
            text
            .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        public static string[] StemTokenize(string text, Hunspell hunspell) =>
            Tokenize(text)
            .Select(word => hunspell.Stem(word).FirstOrDefault() ?? word)
            .ToArray();
    }
}
