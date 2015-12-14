using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordCloudMVVM;
using WordCloudMVVM.Model.Word;

namespace WordCloudTest
{
    [TestClass]
    public class WordWeightToWordFontSizeConverterTest
    {
        [TestMethod]
        public void WordsWeight_Convert_CorrectWordsFontSize()
        {
            IEnumerable<WordWeight> wordsWeight = new List<WordWeight>
            {
                new WordWeight("пошел", 12),
                new WordWeight("сегодня", 1),
                new WordWeight("горка", 34),
                new WordWeight("подъезд", 11),
                new WordWeight("зачем", 13),
                new WordWeight("замок", 3),
                new WordWeight("упал", 2),
                new WordWeight("стул", 6),
                new WordWeight("машина", 1),
            };
            const int maxFontSize = 40;
            IEnumerable<WordFontSize> actualWordsFontSize = WordWeightToWordStyleConverter.Convert(wordsWeight, maxFontSize);
            IEnumerable<WordFontSize> exceptWordsFontSize = new List<WordFontSize>
            {
                new WordFontSize("пошел", 13),
                new WordFontSize("сегодня", 1),
                new WordFontSize("горка", 40),
                new WordFontSize("подъезд", 12),
                new WordFontSize("зачем", 14),
                new WordFontSize("замок", 2),
                new WordFontSize("упал", 1),
                new WordFontSize("стул", 6),
                new WordFontSize("машина", 1)
            };

            Assert.IsTrue(actualWordsFontSize.All(actualWordFontSize => exceptWordsFontSize.First(exceptWordFontSize => exceptWordFontSize.Say == actualWordFontSize.Say).FontSize == actualWordFontSize.FontSize));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MaxFontSizeIs0_Convert_ArgumentException()
        {
            WordWeightToWordStyleConverter.Convert(new List<WordWeight>(), 0);
        }

        [TestMethod]
        public void MaxWeightEqualMinWeight_Convert_FontSizeIs1()
        {
            var actualFontSizeWord = WordWeightToWordStyleConverter.Convert(new List<WordWeight> { new WordWeight("qwer", 1) }, 1);
            Assert.AreEqual(1, actualFontSizeWord.First().FontSize);
        }
    }
}
