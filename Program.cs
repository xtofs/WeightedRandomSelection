
using System.Diagnostics;

internal class Program
{
    private static void Main(string[] args)
    {
        var weights = new Dictionary<string, int>
        {
            ["A"] = 100,
            ["B"] = 100,
            ["C"] = 100,
            ["D"] = 100,
            ["E"] = 100,
            ["F"] = 100,
            ["G"] = 50
        };

        Console.WriteLine("weights: {0:P1}", weights.Format("P"));
        Console.WriteLine();

        Single(1_000_000, weights);
        Double(1_000_000, weights);
    }

    private static void Single(int N, IReadOnlyDictionary<string, int> weightedCollection)
    {
        var wc = new WeightedCollection<string>(weightedCollection);
        var rng = Random.Shared;
        var counter = new Counter<string>();
        var sw = Stopwatch.StartNew();
        Console.WriteLine("number of iterations {0:0,0}", N);
        for (int i = 0; i < N; i++)
        {
            var lbl = wc.Choose(rng);
            counter[lbl] += 1;
        }
        sw.Stop();
        Console.WriteLine("counter: {0:P}", counter);
        Console.WriteLine("elapsed: {0}", sw.Elapsed);
        Console.WriteLine();
    }

    private static void Double(int N, IReadOnlyDictionary<string, int> weightedCollection)
    {
        var wc = new WeightedCollection<string>(weightedCollection);
        var rng = Random.Shared;
        var counter = new Counter<string>();
        var pairCounter = new Counter<string>();
        var sw = Stopwatch.StartNew();
        Console.WriteLine("number of iterations {0:0,0}", N);
        for (int i = 0; i < N; i++)
        {
            var lbl = wc.Choose(2, rng).ToList();
            counter[lbl[0]] += 1;
            counter[lbl[1]] += 1;
            var pair = string.Join("-", lbl.OrderBy(s => s));
            pairCounter[pair] += 1;
        }
        sw.Stop();
        Console.WriteLine("counter: {0:P}", counter);
        // Console.WriteLine("pairs: {0:P}", pairCounter);
        Console.WriteLine("elapsed: {0}", sw.Elapsed);
        Console.WriteLine();
    }
}