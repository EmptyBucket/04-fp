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
        public void TextNewLineWord_Parse_CorrectEnumWordWeight()
        {
            string newLine = Environment.NewLine;
            string[] textNewLineWord = new string[] { "свойственный", "состарившийся", "двор", "свет", "и", "при", "двор" };

            IEnumerable<WordWeight> wordWeightEnum = CountParser.CountParse(textNewLineWord);
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
        public void TextNewLineWord_Parse_CorrectCoutElement()
        {
            string newLine = Environment.NewLine;
            string[] textNewLineWord = new string[] { "свойственный", "состарившийся", "двор", "свет", "и", "при", "двор" };
            IEnumerable<WordWeight> wordWeightEnum = CountParser.CountParse(textNewLineWord);
            var actualCount = wordWeightEnum.Count();
            Assert.AreEqual(6, actualCount);
        }
    }
}
