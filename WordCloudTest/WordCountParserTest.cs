using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NHunspell;
using WordCloudMVVM;
using WordCloudMVVM.Model.WordParse;
using WordCloudMVVM.Model.WordParse.Clean;

namespace WordCloudTest
{
    [TestClass]
    public class WordCountParserTest
    {
        private readonly Hunspell mHunspell;
        private readonly WordParser mParser;

        public WordCountParserTest()
        {
            //не знаю, как в данной ситуации использовать moq
            string pathHunspellDict = Path.Combine(Environment.CurrentDirectory, "HunspellDictionary", "ru_RU.dic");
            string pathHunspellAff = Path.Combine(Environment.CurrentDirectory, "HunspellDictionary", "ru_RU.aff");
            mHunspell = new Hunspell(pathHunspellAff, pathHunspellDict);
            IEnumerable<string> exceptedTokenize = new List<string>
            {
                "свойственный",
                "состаревшемуся",
                "двор",
                "свет",
                "и",
                "пря",
                "двор"
            };
            ITokenizer stemTokenizer = Mock.Of<ITokenizer>(tokenizer => tokenizer.Tokenize(It.IsAny<string>()) == exceptedTokenize);

            mParser = new WordParser(stemTokenizer);
        }

        [TestMethod]
        public void TextNewLineWord_Parse_CorrectEnumWordWeight()
        {
            string newLine = Environment.NewLine;
            string textNewLineWord = $"свойственны{newLine}состаревшемуся{newLine}дворе{newLine}свете{newLine}и{newLine}при{newLine}дворе";

            IEnumerable<WordWeight> wordWeightEnum = mParser.Parse(textNewLineWord);
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
            string textNewLineWord = $"свойственны{newLine}состаревшемуся{newLine}дворе{newLine}свете{newLine}и{newLine}при{newLine}дворе";
            IEnumerable<WordWeight> wordWeightEnum = mParser.Parse(textNewLineWord);
            var actualCount = wordWeightEnum.Count();
            Assert.AreEqual(6, actualCount);
        }

        [TestMethod]
        public void TextLiterature_Parse_CorrectCoutElement()
        {
            string literText = $"которые,./[]  свойственны 1234567890-=<>?:  ;',.состаревшемуся  @&%(дворе  свете !@#$%^&*( + )()!и  при  дворе";

            IEnumerable<WordWeight> wordWeightEnum = mParser.Parse(literText);
            var actualCount = wordWeightEnum.Count();
            Assert.AreEqual(6, actualCount);
        }

        [TestMethod]
        public void TextLiterature_Parse_CorrectEnumWordWeight()
        {
            string literText = $"которые,./[]  свойственны 1234567890-=<>?:  ;',.состаревшемуся  @&%(дворе  свете !@#$%^&*( + )()!и  при  дворе";
            IEnumerable<WordWeight> wordWeightEnum = mParser.Parse(literText);
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
        public void Text_GetUniqueWord_UniqueWord()
        {
            string[] text = new[] { "свойственны", "дворе", "свойственны", "дворе", "свойственны" };
            HashSet<string> exceptUniqueWord = new HashSet<string> { "дворе", "свойственны" };
            HashSet<string> actualUniqueWord = mParser.GetAllUniqueWords(text);
            Assert.IsTrue(exceptUniqueWord.All(word => actualUniqueWord.Contains(word)));
            Assert.AreEqual(actualUniqueWord.Count, exceptUniqueWord.Count);
        }
    }
}
