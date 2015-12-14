using System;
using System.Collections.Generic;
using System.Linq;
using NHunspell;
using WordCloudMVVM.Model.WordParse.Clean;
using WordCloudMVVM.Model.WordParse.Token;

namespace WordCloudMVVM.Model.WordParse
{
    public class StemTokenizer : Tokenizer, IDisposable
    {
        private readonly Hunspell mHunspell;

        public override IEnumerable<string> Tokenize(string text) =>
            base.Tokenize(text)
                .Select(word => mHunspell.Stem(word).FirstOrDefault() ?? word);

        public StemTokenizer(ICleaner cleaner, Hunspell hunspell) : base(cleaner)
        {
            mHunspell = hunspell;
        }

        public void Dispose() =>
            mHunspell.Dispose();
    }
}
