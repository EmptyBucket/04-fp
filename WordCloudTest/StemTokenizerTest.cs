using System;
using System.Collections.Generic;
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
        public void Text_GetAllPrimaryWord_PrimaryWord()
        {
            string text = "свойственны дворе свете";
            IEnumerable<string> exceptWord = new[] { "свойственный", "свет", "двор" };
            IEnumerable<string> actualWord = StemTokenizer.StemTokenize(text, mHunspell);
            Assert.IsTrue(exceptWord.All(word => actualWord.Contains(word)));
        }

        [TestMethod]
        public void Text_Tokenize_AllWord()
        {
            string text = "которые свойственны состаревшемуся в свете и при дворе значительному человеку";
            IEnumerable<string> actualWord = StemTokenizer.Tokenize(text);
            string[] exceptWord = new[] { "которые", "свойственны", "состаревшемуся", "в", "свете", "и", "при", "дворе", "значительному", "человеку" };
            Assert.IsTrue(exceptWord.All(word => actualWord.Contains(word)));
        }
    }
}
