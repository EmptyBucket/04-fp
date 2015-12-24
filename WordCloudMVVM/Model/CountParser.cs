using System.Collections.Generic;
using System.Linq;

namespace WordCloudMVVM.Model
{
    public static class CountParser
    {
        public static HashSet<WordWeight> CountParse(IReadOnlyCollection<string> words)
        {
            var uniqueWords = new HashSet<string>(words);

            var dictCountUniqueWords = uniqueWords
                .ToDictionary(item => item, item => 0);

            foreach (var item in words)
                dictCountUniqueWords[item]++;

            var uniqueWordsWeight = dictCountUniqueWords
                .Select(CountWord => new WordWeight(CountWord.Key, CountWord.Value));

            return new HashSet<WordWeight>(uniqueWordsWeight);
        }
    }
}
