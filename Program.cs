
using System.Diagnostics;

var pdf = CumulativeDistributionFunction.Create(("A", 100), ("B", 100), ("C", 60));

var rng = Random.Shared;
var counts = new Counter<string>();
var sw = Stopwatch.StartNew();
for (int i = 0; i < 10_000_000; i++)
{
    var p = pdf.Pick(rng.NextDouble());
    counts[p] += 1;
}
sw.Stop();
Console.WriteLine("{0:P}", counts);
Console.WriteLine("{0}", sw.Elapsed);
