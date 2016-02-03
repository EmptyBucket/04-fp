namespace WordCloudMVVM.Model.Word
{
    public class WordWeight
    {
        private readonly string _say;
        public string Say { get { return _say; } }

        private readonly int _weight;
        public int Weight { get { return _weight; } }

        public WordWeight(string word, int weight)
        {
            _say = word;
            _weight = weight;
        }
    }
}
