namespace WordCloudMVVM
{
    public class WordWeight
    {
        public string Say { get; }
        public int Weight { get; }

        public WordWeight(string word, int weight)
        {
            Say = word;
            Weight = weight;
        }
    }
}
