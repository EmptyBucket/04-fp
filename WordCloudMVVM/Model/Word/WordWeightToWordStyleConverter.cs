using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace WordCloudMVVM.Model.Word
{
    public static class WordWeightToWordStyleConverter
    {
        public static WordStyle[] Convert(IReadOnlyCollection<WordWeight> wordsWeight, int MaxFontSize)
        {
            WordWeight[] wordsWeightArray = wordsWeight.ToArray();
            if (MaxFontSize == 0)
                throw new ArgumentException("Max font size is 0");
            int minWeight = 0;
            int maxWeight = 0;
            if (wordsWeightArray.Any())
            {
                minWeight = wordsWeightArray.Min(wordWeight => wordWeight.Weight);
                maxWeight = wordsWeightArray.Max(wordWeight => wordWeight.Weight);
            }

            Random rand = new Random();

            var wordsFontSize = wordsWeightArray
                .Select(wordWeight =>
                {
                    int fontSize = wordWeight.Weight > minWeight ? (MaxFontSize * (wordWeight.Weight - minWeight)) / (maxWeight - minWeight) : 1;
                    fontSize = fontSize == 0 ? 1 : fontSize;
                    Color color = new Color()
                    {
                        B = (byte)rand.Next(0, 255),
                        R = (byte)rand.Next(0, 255),
                        G = (byte)rand.Next(0, 255),
                        A = 255
                    };
                    return new WordStyle(wordWeight.Say, fontSize, color);
                })
                .ToArray();
            return wordsFontSize;
        }
    }
}
