using System.Collections.Generic;
using System.Linq;

namespace WordCloudMVVM.Model
{
    public class InspectWords
    {
        public WordWeight[] GoodWords { get; }

        public WordWeight[] BadWords { get; }

        public InspectWords(IReadOnlyCollection<WordWeight> goodWords, IReadOnlyCollection<WordWeight> badWords)
        {
            GoodWords = goodWords.ToArray();
            BadWords = badWords.ToArray();
        }
    }
}
