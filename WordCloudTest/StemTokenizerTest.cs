using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHunspell;
using WordCloudMVVM.Model;

namespace WordCloudTest
{
    [TestClass]
    public class StemTokenizerTest
    {
        private readonly Hunspell _hunspell;

        public StemTokenizerTest()
        {
            var pathHunspellDict = Path.Combine(Environment.CurrentDirectory, "HunspellDictionary", "ru_RU.dic");
            var pathHunspellAff = Path.Combine(Environment.CurrentDirectory, "HunspellDictionary", "ru_RU.aff");

            _hunspell = new Hunspell(pathHunspellAff, pathHunspellDict);
        }

        [TestMethod]
        public void Text_StemTokenize_StemAllWords()
        {
            const string text = "свойственны дворе свете";
            var exceptWord = new[] { "свойственный", "свет", "двор" };
            var actualWord = StemTokenizer.StemTokenize(text, _hunspell);
            Assert.IsTrue(exceptWord.All(word => actualWord.Contains(word)));
        }

        [TestMethod]
        public void Text_Tokenize_AllWords()
        {
            const string text = "которые свойственны состаревшемуся в свете и при дворе значительному человеку";
            var actualWord = StemTokenizer.Tokenize(text);
            var exceptWord = new[] { "которые", "свойственны", "состаревшемуся", "в", "свете", "и", "при", "дворе", "значительному", "человеку" };
            Assert.IsTrue(exceptWord.All(word => actualWord.Contains(word)));
        }
    }
}
