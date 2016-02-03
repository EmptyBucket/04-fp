using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace WordCloudMVVM.Model.Word
{
    public static class WordWeightToWordStyleConverter
    {
        public static WordStyle[] Convert(IReadOnlyCollection<WordWeight> wordsWeight, int maxFontSize)
        {
            var wordsWeightArray = wordsWeight.ToArray();
            if (maxFontSize == 0)
                throw new ArgumentException("Max font size is 0");
            var minWeight = 0;
            var maxWeight = 0;
            if (wordsWeightArray.Any())
            {
                minWeight = wordsWeightArray.Min(wordWeight => wordWeight.Weight);
                maxWeight = wordsWeightArray.Max(wordWeight => wordWeight.Weight);
            }

            var rand = new Random();

            var wordsFontSize = wordsWeightArray
                .Select(wordWeight =>
                {
                    var fontSize = wordWeight.Weight > minWeight ? maxFontSize * (wordWeight.Weight - minWeight) / (maxWeight - minWeight) : 1;
                    fontSize = fontSize == 0 ? 1 : fontSize;
                    var color = new Color
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
