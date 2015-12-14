using System;
using System.Collections.Generic;
using WordCloudMVVM.Model.WordParse.Clean;

namespace WordCloudMVVM.Model.WordParse.Token
{
    public class Tokenizer : ITokenizer
    {
        private readonly ICleaner mCleaner;

        public virtual IEnumerable<string> Tokenize(string text)
        {
            return mCleaner
                .Clean(text)
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        }

        public Tokenizer(ICleaner cleaner)
        {
            mCleaner = cleaner;
        }
    }
}
