using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordCloudMVVM.Model;

namespace WordCloudTest
{
    [TestClass]
    public class TextCleanerTest
    {
        [TestMethod]
        public void DirtyText_Clear_CleanText()
        {
            string dirtyText = "’'[],(){}⟨⟩<>:‒…!.‐-?„“«»“”‘’‹qwe›;1234567890_-+=/|@#$%^&*\"\r\n\t";
            string cleanText = Cleaner.Clean(dirtyText);
            string exceptCleanText = " qwe ";
            Assert.AreEqual(exceptCleanText, cleanText);
        }
    }
}
