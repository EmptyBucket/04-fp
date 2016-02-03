using System.IO;
using System.Text;

namespace WordCloudMVVM.Model
{
    public static class TextReader
    {
        public static string Read(Stream stream, Encoding encoding)
        {
            using (var reader = new StreamReader(stream, encoding))
                return reader.ReadToEnd();
        }
    }
}
