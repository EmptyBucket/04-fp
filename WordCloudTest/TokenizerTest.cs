using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WordCloudMVVM.Model.WordParse.Clean;
using WordCloudMVVM.Model.WordParse.Token;

namespace WordCloudTest
{
    [TestClass]
    public class TokenizerTest
    {
        private readonly Tokenizer mTokenizer;

        public TokenizerTest()
        {
            string clearText = "которые свойственны состаревшемуся в свете и при дворе значительному человеку";
            ICleaner textCleaner = Mock.Of<ICleaner>(cleaner => cleaner.Clean(It.IsAny<string>()) == clearText);

            mTokenizer = new Tokenizer(textCleaner);
        }

        [TestMethod]
        public void Text_Tokenize_AllWord()
        {
            string text = "которые,  свойственны  .состаревшемуся  в,  свете  и . при  дворе  ,значительному человеку";
            IEnumerable<string> actualWord = mTokenizer.Tokenize(text);
            string[] exceptWord = new[] { "которые", "свойственны", "состаревшемуся", "в", "свете", "и", "при", "дворе", "значительному", "человеку" };
            Assert.IsTrue(exceptWord.All(word => actualWord.Contains(word)));
        }
    }
}
