using System.Collections.Generic;
using System.Linq;
using WordCloudMVVM.Model.Word;

namespace WordCloudMVVM.Model
{
    public class InspectWords
    {
	    private readonly WordWeight[] _goodWords;
        public WordWeight[] GoodWords { get { return _goodWords; } }

		private readonly WordWeight[] _badWords;
		public WordWeight[] BadWords { get { return _badWords; } }

        public InspectWords(IEnumerable<WordWeight> goodWords, IEnumerable<WordWeight> badWords)
        {
            _goodWords = goodWords.ToArray();
            _badWords = badWords.ToArray();
        }
    }
}
