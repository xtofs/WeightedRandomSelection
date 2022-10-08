
using System.Diagnostics;

var pdf = CumulativeDistributionFunction.Create(
    ("A", 100), ("B", 100), ("C", 100),
    ("D", 100), ("E", 100), ("F", 100),
    ("G", 60)
);

var rng = Random.Shared;
var counter = new Counter<string>();
var sw = Stopwatch.StartNew();
for (int i = 0; i < 10_000_000; i++)
{
    var p = rng.SelectFrom(pdf);
    counter[p] += 1;
}
sw.Stop();
Console.WriteLine("{0:P}", counter);
Console.WriteLine("{0}", sw.Elapsed);
