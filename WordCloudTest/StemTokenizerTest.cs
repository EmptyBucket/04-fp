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
        private readonly Hunspell mHunspell;

        public StemTokenizerTest()
        {
            string pathHunspellDict = Path.Combine(Environment.CurrentDirectory, "HunspellDictionary", "ru_RU.dic");
            string pathHunspellAff = Path.Combine(Environment.CurrentDirectory, "HunspellDictionary", "ru_RU.aff");

            mHunspell = new Hunspell(pathHunspellAff, pathHunspellDict);
        }

        [TestMethod]
        public void Text_StemTokenize_StemAllWords()
        {
            string text = "свойственны дворе свете";
            var exceptWord = new[] { "свойственный", "свет", "двор" };
            var actualWord = StemTokenizer.StemTokenize(text, mHunspell);
            Assert.IsTrue(exceptWord.All(word => actualWord.Contains(word)));
        }

        [TestMethod]
        public void Text_Tokenize_AllWords()
        {
            string text = "которые свойственны состаревшемуся в свете и при дворе значительному человеку";
            var actualWord = StemTokenizer.Tokenize(text);
            string[] exceptWord = new[] { "которые", "свойственны", "состаревшемуся", "в", "свете", "и", "при", "дворе", "значительному", "человеку" };
            Assert.IsTrue(exceptWord.All(word => actualWord.Contains(word)));
        }
    }
}
