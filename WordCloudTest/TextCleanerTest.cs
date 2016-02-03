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
            const string dirtyText = "’'[],(){}⟨⟩<>:‒…!.‐-?„“«»“”‘’‹qwe›;1234567890_-+=/|@#$%^&*\"\r\n\t";
            var cleanText = Cleaner.Clean(dirtyText);
            const string exceptCleanText = " qwe ";
            Assert.AreEqual(exceptCleanText, cleanText);
        }

        [TestMethod]
        public void CleanText_Clear_SameText()
        {
            const string dirtyText = "qwer sadf qwera sdf xcv asdf asdfqwer asdf wer";
            var cleanText = Cleaner.Clean(dirtyText);
            const string exceptCleanText = "qwer sadf qwera sdf xcv asdf asdfqwer asdf wer";
            Assert.AreEqual(exceptCleanText, cleanText);
        }

        [TestMethod]
        public void EmptyText_Clear_SameText()
        {
            var dirtyText = string.Empty;
            var cleanText = Cleaner.Clean(dirtyText);
            var exceptCleanText = string.Empty;
            Assert.AreEqual(exceptCleanText, cleanText);
        }
    }
}
