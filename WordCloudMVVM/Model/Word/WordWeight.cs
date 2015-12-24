namespace WordCloudMVVM
{
    public class WordWeight
    {
	    private string say;
        public string Say { get { return say; } }

	    private int weight;
        public int Weight { get { return weight; } }

        public WordWeight(string word, int weight)
        {
            say = word;
            this.weight = weight;
        }
    }
}
