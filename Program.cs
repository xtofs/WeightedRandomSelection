
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
            ["G"] = 60
        };


        var pdf = CumulativeDistributionFunction.Create(weights);
        Console.WriteLine("weights: {0:P1}", weights.Format("P"));
        Console.WriteLine();

        Single(10_000_000, pdf);
        Double(1_000_000, pdf);
    }

    private static void Single(int N, CumulativeDistributionFunction<string> pdf)
    {
        var rng = Random.Shared;
        var counter = new Counter<string>();
        var sw = Stopwatch.StartNew();
        Console.WriteLine("number of iterations {0:0,0}", N);
        for (int i = 0; i < N; i++)
        {
            var lbl = rng.Choose(pdf);
            counter[lbl] += 1;
        }
        sw.Stop();
        Console.WriteLine("counter: {0:P}", counter);
        Console.WriteLine("elapsed: {0}", sw.Elapsed);
        Console.WriteLine();
    }

    private static void Double(int N, CumulativeDistributionFunction<string> pdf)
    {
        var rng = Random.Shared;
        var counter = new Counter<string>();
        var pairCounter = new Counter<string>();
        var sw = Stopwatch.StartNew();
        Console.WriteLine("number of iterations {0:0,0}", N);
        for (int i = 0; i < N; i++)
        {
            var lbl = rng.ChooseMany(2, pdf);
            counter[lbl[0]] += 1;
            counter[lbl[1]] += 1;
            var pair = string.Join("-", lbl.OrderBy(s => s));
            pairCounter[pair] += 1;
        }
        sw.Stop();
        Console.WriteLine("counter: {0:P}", counter);
        Console.WriteLine("pairs: {0:P}", pairCounter);
        Console.WriteLine("elapsed: {0}", sw.Elapsed);
        Console.WriteLine();
    }
}