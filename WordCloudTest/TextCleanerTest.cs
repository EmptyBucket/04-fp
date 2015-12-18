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

        [TestMethod]
        public void CleanText_Clear_SameText()
        {
            string dirtyText = "qwer sadf qwera sdf xcv asdf asdfqwer asdf wer";
            string cleanText = Cleaner.Clean(dirtyText);
            string exceptCleanText = "qwer sadf qwera sdf xcv asdf asdfqwer asdf wer";
            Assert.AreEqual(exceptCleanText, cleanText);
        }

        [TestMethod]
        public void EmptyText_Clear_SameText()
        {
            string dirtyText = string.Empty;
            string cleanText = Cleaner.Clean(dirtyText);
            string exceptCleanText = string.Empty;
            Assert.AreEqual(exceptCleanText, cleanText);
        }
    }
}
