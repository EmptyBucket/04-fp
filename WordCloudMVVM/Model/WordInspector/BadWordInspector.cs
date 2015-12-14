using System;
using System.Collections.Generic;
using System.IO;

namespace WordCloudMVVM.Model.WordInspector
{
    public class BadWordInspector : IWordInspector
    {
        private readonly HashSet<string> mBadWords;

        public BadWordInspector(Stream stream)
        {
            using (StreamReader reader = new StreamReader(stream))
                mBadWords = new HashSet<string>(
                    reader
                    .ReadToEnd()
                    .Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries));
        }

        public BadWordInspector(string path)
        {
            mBadWords = new HashSet<string>(File.ReadAllLines(path));
        }

        public bool IsBad(string word) =>
            mBadWords.Contains(word);
    }
}
