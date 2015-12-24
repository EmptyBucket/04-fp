using System;
using System.Collections.Generic;
using System.Linq;
using NHunspell;

namespace WordCloudMVVM.Model
{
    public static class StemTokenizer
    {
	    public static IEnumerable<string> Tokenize(string text)
	    {
            return text
				.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
		    
	    }

	    public static IEnumerable<string> StemTokenize(string text, Hunspell hunspell)
	    {
			return Tokenize(text)
					.Select(word => hunspell.Stem(word).FirstOrDefault() ?? word);
	    }
    }
}
