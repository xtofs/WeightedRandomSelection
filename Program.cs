
var pdf = DiscreteProbabilityDensity.Create(("A", 100), ("B", 100), ("C", 60));

var rng = Random.Shared;
var counts = new Counter<string>();
for (int i = 0; i < 1_000_000; i++)
{
    var p = pdf.Pick(rng.NextDouble());
    counts[p] += 1;
}

Console.WriteLine("{0:P}", counts);
