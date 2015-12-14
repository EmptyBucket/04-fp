using System.IO;

namespace WordCloudMVVM.Model.Read
{
    public interface ITextReader
    {
        string ReadAll(Stream stream);

        string ReadAll(string path);
    }
}
