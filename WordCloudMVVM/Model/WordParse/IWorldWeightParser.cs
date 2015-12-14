using System.Collections.Generic;

namespace WordCloudMVVM
{
    public interface IWordWeightParser
    {
        IEnumerable<WordWeight> Parse(string text);
    }
}
