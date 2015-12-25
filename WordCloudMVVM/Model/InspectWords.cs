using System.Collections.Generic;
using System.Linq;

namespace WordCloudMVVM.Model
{
    public class InspectWords
    {
	    private readonly WordWeight[] goodWords;
        public WordWeight[] GoodWords { get { return goodWords; } }

		private readonly WordWeight[] badWords;
		public WordWeight[] BadWords { get { return badWords; } }

        public InspectWords(IEnumerable<WordWeight> goodWords, IEnumerable<WordWeight> badWords)
        {
            this.goodWords = goodWords.ToArray();
            this.badWords = badWords.ToArray();
        }
    }
}
