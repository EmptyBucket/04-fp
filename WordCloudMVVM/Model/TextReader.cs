using System.IO;

namespace WordCloudMVVM.Model
{
    static class TextReader
    {
        public static string Read(Stream stream)
        {
            using (StreamReader reader = new StreamReader(stream))
                return reader.ReadToEnd();
        }
    }
}
