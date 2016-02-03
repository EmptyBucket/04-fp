using System.Collections.Generic;
using System.Linq;
using WordCloudMVVM.Model.Word;

namespace WordCloudMVVM.Model
{
    public static class CountWordParser
    {
        public static HashSet<WordWeight> Parse(IReadOnlyCollection<string> words)
        {
            var uniqueWords = new HashSet<string>(words);

            var dictCountUniqueWords = uniqueWords
                .ToDictionary(item => item, item => 0);

            foreach (var item in words)
                dictCountUniqueWords[item]++;

            var uniqueWordsWeight = dictCountUniqueWords
                .Select(countWord => new WordWeight(countWord.Key, countWord.Value));

            return new HashSet<WordWeight>(uniqueWordsWeight);
        }
    }
}
