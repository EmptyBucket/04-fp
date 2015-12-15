using System.Collections.Generic;
using System.Linq;

namespace WordCloudMVVM.Model
{
    public static class CountParser
    {
        public static IEnumerable<WordWeight> CountParse(IEnumerable<string> words)
        {
            HashSet<string> uniqueWords = new HashSet<string>(words);

            Dictionary<string, int> dictCountUniqueWords = uniqueWords
                .ToDictionary(item => item, item => 0);

            foreach (var item in words)
                dictCountUniqueWords[item]++;

            return dictCountUniqueWords
                .Select(CountWord => new WordWeight(CountWord.Key, CountWord.Value));
        }
    }
}
