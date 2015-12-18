using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHunspell;
using WordCloudMVVM;
using WordCloudMVVM.Model;

namespace WordCloudTest
{
    [TestClass]
    public class WordCountParserTest
    {
        private readonly Hunspell mHunspell;

        public WordCountParserTest()
        {
            string pathHunspellDict = Path.Combine(Environment.CurrentDirectory, "HunspellDictionary", "ru_RU.dic");
            string pathHunspellAff = Path.Combine(Environment.CurrentDirectory, "HunspellDictionary", "ru_RU.aff");
            mHunspell = new Hunspell(pathHunspellAff, pathHunspellDict);
        }

        [TestMethod]
        public void Words_Parse_CorrectEnumWordWeight()
        {
            string[] words = new string[] { "свойственный", "состарившийся", "двор", "свет", "и", "при", "двор" };

            IEnumerable<WordWeight> wordWeightEnum = CountParser.CountParse(words);
            var except = new WordWeight("свойственный", 1);
            var actual = wordWeightEnum.First(wordWeight => wordWeight.Say == "свойственный");
            Assert.AreEqual(except.Say, actual.Say);
            Assert.AreEqual(except.Weight, actual.Weight);
            except = new WordWeight("свет", 1);
            actual = wordWeightEnum.First(wordWeight => wordWeight.Say == "свет");
            Assert.AreEqual(except.Say, actual.Say);
            Assert.AreEqual(except.Weight, actual.Weight);
            except = new WordWeight("двор", 2);
            actual = wordWeightEnum.First(wordWeight => wordWeight.Say == "двор");
            Assert.AreEqual(except.Say, actual.Say);
            Assert.AreEqual(except.Weight, actual.Weight);
        }

        [TestMethod]
        public void OneWordsManyTimes_Parse_CorrectWordsWeight()
        {
            var words = Enumerable.Repeat("свет", 100);
            var actual = CountParser.CountParse(words);
            Assert.IsTrue(actual.All(wordWeight => wordWeight.Say == "свет"));
            Assert.AreEqual(1, actual.Count);
        }

        [TestMethod]
        public void OneWordOneTimes_Parse_CorrectWordWeight()
        {
            var words = new List<string> { "свет" };
            var actual = CountParser.CountParse(words);
            Assert.IsTrue(actual.All(wordWeight => wordWeight.Say == "свет"));
            Assert.AreEqual(1, actual.Count);
        }

        [TestMethod]
        public void Words_Parse_CorrectCoutElement()
        {
            string[] words = new string[] { "свойственный", "состарившийся", "двор", "свет", "и", "при", "двор" };
            IEnumerable<WordWeight> wordWeightEnum = CountParser.CountParse(words);
            var actualCount = wordWeightEnum.Count();
            Assert.AreEqual(6, actualCount);
        }
    }
}
