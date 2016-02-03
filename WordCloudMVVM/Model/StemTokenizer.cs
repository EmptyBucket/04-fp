using System;
using System.Linq;
using NHunspell;

namespace WordCloudMVVM.Model
{
    public static class StemTokenizer
    {
	    public static string[] Tokenize(string text)
	    {
            return text
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
	    }

	    public static string[] StemTokenize(string text, Hunspell hunspell)
	    {
			return Tokenize(text)
			    .Select(word => hunspell.Stem(word).FirstOrDefault() ?? word)
			    .ToArray();
	    }
    }
}
