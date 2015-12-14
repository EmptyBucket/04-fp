using System.IO;

namespace WordCloudMVVM.Model.Read
{
    public class TXTReader : ITextReader
    {
        public string ReadAll(Stream stream)
        {
            using (StreamReader reader = new StreamReader(stream))
                return reader.ReadToEnd();
        }

        public string ReadAll(string path) =>
            File.ReadAllText(path);
    }
}
