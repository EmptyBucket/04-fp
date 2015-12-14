using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordCloudMVVM.Model.WordParse.Clean;

namespace WordCloudTest
{
    [TestClass]
    public class TextCleanerTest
    {
        private readonly WordCloudMVVM.Model.WordParse.Clean.TextCleaner mCleaner;

        public TextCleanerTest()
        {
            mCleaner = new WordCloudMVVM.Model.WordParse.Clean.TextCleaner();
        }

        [TestMethod]
        public void DirtyText_Clear_CleanText()
        {
            string dirtyText = "’'[],(){}⟨⟩<>:‒…!.‐-?„“«»“”‘’‹qwe›;1234567890_-+=/|@#$%^&*\"\r\n\t";
            string cleanText = mCleaner.Clean(dirtyText);
            string exceptCleanText = " qwe ";
            Assert.AreEqual(exceptCleanText, cleanText);
        }
    }
}
