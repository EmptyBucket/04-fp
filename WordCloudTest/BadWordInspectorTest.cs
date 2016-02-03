using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordCloudMVVM.Model;

namespace WordCloudTest
{
    [TestClass]
    public class BadWordInspectorTest
    {
        private readonly HashSet<string> _badWords;

        public BadWordInspectorTest()
        {
            _badWords = new HashSet<string>("о он его него ему нему его им ним нём она её".Split(' '));
        }

        [TestMethod]
        public void BadAndGoodWords_IsBad_GoodWords()
        {
            var lines = _badWords.ToList();
            lines.Add("пошел");
            const string exceptWord = "пошел";
            var actualWord = lines.Where(line => !BadWordInspector.IsBad(line, _badWords)).ToList();
            Assert.IsTrue(actualWord.Count == 1);
            Assert.AreEqual(exceptWord, actualWord[0]);
        }

        [TestMethod]
        public void TextOnlyBadWords_IsBad_Empty()
        {
            var lines = _badWords.ToList();
            var actualWord = lines.Where(line => !BadWordInspector.IsBad(line, _badWords)).ToList();
            Assert.IsTrue(actualWord.Count == 0);
        }
    }
}
