using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NHunspell;
using WordCloudMVVM.Model.WordParse;
using WordCloudMVVM.Model.WordParse.Clean;

namespace WordCloudTest
{
    [TestClass]
    public class StemTokenizerTest
    {
        private readonly StemTokenizer mStemTokenizer;
        private readonly string mText;

        public StemTokenizerTest()
        {
            mText = "свойственны дворе свете";
            ICleaner textCleaner = Mock.Of<ICleaner>(cleaner => cleaner.Clean(It.IsAny<string>()) == mText);

            string pathHunspellDict = Path.Combine(Environment.CurrentDirectory, "HunspellDictionary", "ru_RU.dic");
            string pathHunspellAff = Path.Combine(Environment.CurrentDirectory, "HunspellDictionary", "ru_RU.aff");

            Hunspell hunspell = new Hunspell(pathHunspellAff, pathHunspellDict);

            mStemTokenizer = new StemTokenizer(textCleaner, hunspell);
        }

        [TestMethod]
        public void Text_GetAllPrimaryWord_PrimaryWord()
        {
            IEnumerable<string> exceptWord = new[] { "свойственный", "свет", "двор" };
            IEnumerable<string> actualWord = mStemTokenizer.Tokenize(mText);
            Assert.IsTrue(exceptWord.All(word => actualWord.Contains(word)));
        }
    }
}
