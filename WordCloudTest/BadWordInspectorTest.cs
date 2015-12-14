using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordCloudMVVM.Model.WordInspector;

namespace WordCloudTest
{
    [TestClass]
    public class BadWordInspectorTest
    {
        private readonly BadWordInspector mBadWordInspector;
        private readonly string mBadWords;

        private Stream GenerateStreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        public BadWordInspectorTest()
        {
            mBadWords = $"о он его него ему нему его им ним нём она её".Replace(" ", Environment.NewLine);

            using (Stream memoryStream = GenerateStreamFromString(mBadWords))
                mBadWordInspector = new BadWordInspector(memoryStream);
        }

        [TestMethod]
        public void TextBadAndGoodWord_IsBad_TextGoodWord()
        {
            List<string> lines = mBadWords
                .Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .ToList();
            lines.Add("пошел");
            var exceptWord = "пошел";
            var actualWord = lines.Where(line => !mBadWordInspector.IsBad(line)).ToList();
            Assert.IsTrue(actualWord.Count == 1);
            Assert.AreEqual(exceptWord, actualWord[0]);
        }
    }
}
