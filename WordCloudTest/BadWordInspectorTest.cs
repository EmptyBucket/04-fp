using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordCloudMVVM.Model.WordInspector;

namespace WordCloudTest
{
    [TestClass]
    public class BadWordInspectorTest
    {
        private readonly HashSet<string> mBadWords;

        public BadWordInspectorTest()
        {
            mBadWords = new HashSet<string>($"о он его него ему нему его им ним нём она её".Split(' '));
        }

        [TestMethod]
        public void TextBadAndGoodWord_IsBad_TextGoodWord()
        {
            List<string> lines = mBadWords.ToList();
            lines.Add("пошел");
            var exceptWord = "пошел";
            var actualWord = lines.Where(line => !BadWordInspector.IsBad(line, mBadWords)).ToList();
            Assert.IsTrue(actualWord.Count == 1);
            Assert.AreEqual(exceptWord, actualWord[0]);
        }
    }
}
