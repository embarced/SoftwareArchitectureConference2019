using System;
using System.Linq;
using System.Threading.Tasks;
using GitHub;

namespace ScientistSample.Client
{
    internal class MyResultPublisher : IResultPublisher
    {
        public Task Publish<T, TClean>(Result<T, TClean> result)
        {
            if (result.Candidates.Count > 0)
            {
                if (result.Mismatched)
                {
                    Console.ForegroundColor = ConsoleColor.Red;                  
                }
                Console.WriteLine(string.Format("Default Result: {0}\t\t\tCandidate Result: {1}", result.Control.Value, result.Candidates.First().Value));
                Console.WriteLine(string.Format("Default Duration: {0}\tCandidate Duration: {1}", result.Control.Duration, result.Candidates.First().Duration));
                var performanceDiff = result.Control.Duration - result.Candidates.First().Duration;
                var performanceDiffPercent = Math.Round((performanceDiff / result.Control.Duration) * 100, 2);
                Console.WriteLine(string.Format("Performance benefit of Candidate: {0} ({1}%)", performanceDiff, performanceDiffPercent));
                Console.ForegroundColor = ConsoleColor.Black;
            }
            return Task.FromResult(0);
        }
    }
}