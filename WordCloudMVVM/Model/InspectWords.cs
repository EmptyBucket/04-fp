using System.Collections.Generic;
using System.Linq;

namespace WordCloudMVVM.Model
{
    public class InspectWords
    {
        public IEnumerable<WordWeight> GoodWords { get; }

        public IEnumerable<WordWeight> BadWords { get; }

        public InspectWords(IEnumerable<WordWeight> goodWords, IEnumerable<WordWeight> badWords)
        {
            GoodWords = goodWords;
            BadWords = badWords;
        }
    }
}
