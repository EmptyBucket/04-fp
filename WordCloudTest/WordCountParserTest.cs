using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordCloudMVVM.Model;
using WordCloudMVVM.Model.Word;

namespace WordCloudTest
{
    [TestClass]
    public class WordCountParserTest
    {
        [TestMethod]
        public void Words_Parse_CorrectEnumWordWeight()
        {
            var words = new[] { "свойственный", "состарившийся", "двор", "свет", "и", "при", "двор" };

            var wordWeightEnum = CountWordParser.Parse(words);
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
            var words = Enumerable.Repeat("свет", 100).ToArray();
            var actual = CountWordParser.Parse(words);
            Assert.IsTrue(actual.All(wordWeight => wordWeight.Say == "свет"));
            Assert.AreEqual(1, actual.Count);
        }

        [TestMethod]
        public void OneWordOneTimes_Parse_CorrectWordWeight()
        {
            var words = new List<string> { "свет" };
            var actual = CountWordParser.Parse(words);
            Assert.IsTrue(actual.All(wordWeight => wordWeight.Say == "свет"));
            Assert.AreEqual(1, actual.Count);
        }

        [TestMethod]
        public void Words_Parse_CorrectCoutElement()
        {
            var words = new[] { "свойственный", "состарившийся", "двор", "свет", "и", "при", "двор" };
            var wordWeightEnum = CountWordParser.Parse(words);
            var actualCount = wordWeightEnum.Count;
            Assert.AreEqual(6, actualCount);
        }
    }
}
