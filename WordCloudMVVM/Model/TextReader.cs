using System.IO;
using System.Text;

namespace WordCloudMVVM.Model
{
    static class TextReader
    {
        public static string Read(Stream stream, Encoding encoding)
        {
            using (StreamReader reader = new StreamReader(stream, encoding))
                return reader.ReadToEnd();
        }
    }
}
