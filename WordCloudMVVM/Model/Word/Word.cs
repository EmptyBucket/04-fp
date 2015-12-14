namespace WordCloudMVVM.Model.Word
{
    public class Word : IWord
    {
        public string Say { get; private set; }

        public Word(string word)
        {
            Say = word;
        }
    }
}
