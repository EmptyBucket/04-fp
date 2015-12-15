using System;
using System.Collections.Generic;
using System.Linq;

namespace WordCloudMVVM.Model
{
    static class Parser
    {
        public static IEnumerable<WordWeight> Parse(string text, Func<string, string> Clean, Func<string, IEnumerable<string>> Tokenize)
        {
            string cleanText = Clean(text);

            IEnumerable<string> words = Tokenize(cleanText);

            HashSet<string> uniquePrimaryWords = new HashSet<string>(words);

            Dictionary<string, int> dictCountUniqueWords = uniquePrimaryWords
                .ToDictionary(item => item, item => 0);

            foreach (var item in words)
                dictCountUniqueWords[item]++;

            return dictCountUniqueWords
                .Select(CountWord => new WordWeight(CountWord.Key, CountWord.Value));
        }
    }
}
