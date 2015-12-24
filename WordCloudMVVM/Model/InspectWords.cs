using System.Collections.Generic;
using System.Linq;

namespace WordCloudMVVM.Model
{
    public class InspectWords
    {
	    private IEnumerable<WordWeight> goodWords;
        public IEnumerable<WordWeight> GoodWords { get { return goodWords; } }

	    private IEnumerable<WordWeight> badWords;
        public IEnumerable<WordWeight> BadWords { get { return badWords; } }

        public InspectWords(IEnumerable<WordWeight> goodWords, IEnumerable<WordWeight> badWords)
        {
            this.goodWords = goodWords;
			this.badWords = badWords;
        }
    }
}
