using System.Collections.Generic;

namespace WordCloudMVVM.Model
{
    public static class BadWordInspector
    {
	    public static bool IsBad(string word, HashSet<string> badWords)
	    {
			return badWords.Contains(word);
	    }
    }
}
