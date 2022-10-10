
using System.Diagnostics;

var weights = new[] { ("A", 100), ("B", 100), ("C", 100),
    ("D", 100), ("E", 100), ("F", 100),
    ("G", 60)};
var pdf = CumulativeDistributionFunction.Create(weights);
Console.WriteLine("weights {0:P1}", weights.Format("P"));

var rng = Random.Shared;
var counter = new Counter<string>();
var sw = Stopwatch.StartNew();
const int N = 10_000_000;
Console.WriteLine("number of iterations {0:0,0}", N);
for (int i = 0; i < N; i++)
{
    var lbl = rng.SelectFrom(pdf);
    counter[lbl] += 1;
}
sw.Stop();
Console.WriteLine("counter: {0:P}", counter);
Console.WriteLine("elapsed: {0}", sw.Elapsed);
