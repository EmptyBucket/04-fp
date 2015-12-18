using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordCloudMVVM;
using WordCloudMVVM.Model;
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
            IEnumerable<WordStyle> actualWordsStyel = WordWeightToWordStyleConverter.Convert(wordsWeight, maxFontSize);
            IEnumerable<WordStyle> exceptedWordsStyle = new List<WordStyle>
            {
                new WordStyle("пошел", 13),
                new WordStyle("сегодня", 1),
                new WordStyle("горка", 40),
                new WordStyle("подъезд", 12),
                new WordStyle("зачем", 14),
                new WordStyle("замок", 2),
                new WordStyle("упал", 1),
                new WordStyle("стул", 6),
                new WordStyle("машина", 1)
            };

            Assert.IsTrue(actualWordsStyel
                .All(actualWordFontSize => exceptedWordsStyle.First(exceptWordFontSize => exceptWordFontSize.Say == actualWordFontSize.Say).FontSize == actualWordFontSize.FontSize));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MaxFontSizeIs0_Convert_ArgumentException()
        {
            WordWeightToWordStyleConverter.Convert(new List<WordWeight>(), 0);
        }

        [TestMethod]
        public void WordWeightAndSmallMaxFontSize_Convert_FontSizeIs1()
        {
            var actualFontSizeWord = WordWeightToWordStyleConverter.Convert(new List<WordWeight> { new WordWeight("qwer", 1) }, 1);
            Assert.AreEqual(1, actualFontSizeWord.First().FontSize);
        }

        [TestMethod]
        public void WordWeightAndBigMaxFontSize_Convert_FontSizeIsMax()
        {
            var actualFontSizeWord = WordWeightToWordStyleConverter.Convert(new List<WordWeight> { new WordWeight("asd", 2), new WordWeight("qwer", 1) }, 100);
            Assert.AreEqual(100, actualFontSizeWord.First().FontSize);
        }

        [TestMethod]
        public void WordsWeight_Convert_CountWordWeightEqualCountWordStyle()
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
            List<WordStyle> actualWordsStyle = WordWeightToWordStyleConverter.Convert(wordsWeight, maxFontSize).ToList();
            List<WordStyle> exceptedWordStyle = new List<WordStyle>
            {
                new WordStyle("пошел", 13),
                new WordStyle("сегодня", 1),
                new WordStyle("горка", 40),
                new WordStyle("подъезд", 12),
                new WordStyle("зачем", 14),
                new WordStyle("замок", 2),
                new WordStyle("упал", 1),
                new WordStyle("стул", 6),
                new WordStyle("машина", 1)
            };

            Assert.AreEqual(exceptedWordStyle.Count(), actualWordsStyle.Count);
        }
    }
}
