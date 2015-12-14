using System.Collections.Generic;

namespace WordCloudMVVM.Model.WordParse
{
    public interface ITokenizer
    {
        IEnumerable<string> Tokenize(string text);
    }
}
