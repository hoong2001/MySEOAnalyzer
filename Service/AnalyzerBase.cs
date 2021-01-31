using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StopWord;
using ViewModel;
using TextDiscovery;

namespace Service
{
    public abstract class AnalyzerBase
    {
        protected List<Result> CountWordResults(string text)
        {
            var removedStopWord = text.RemoveStopWords("en"); // remove english stop word

            var textSlice = TextSlicer.Default;
            var slices = textSlice.CreateSlices(source: removedStopWord)
                .Where(x => x.IsToken); // return only word

            var distinctWords = slices.Select(selector => new { Text = selector.Text })
                .Distinct(); // retrieve only distinct word

            List<Result> results = new List<Result>();
            foreach (var word in distinctWords) // Each word counting.
            {
                int count = slices.Count(x => string.Compare(x.Text, word.Text, true) == 0);
                results.Add(new Result()
                {
                    Word = word.Text,
                    Count = count
                });
            }

            // Collection of word count
            return results;

        }
    }
}
