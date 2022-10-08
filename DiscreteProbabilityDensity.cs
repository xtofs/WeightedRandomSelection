static class DiscreteProbabilityDensity
{
    public static DiscreteProbabilityDensity<K> Create<K>(IEnumerable<(K, int)> items) where K : notnull
    {
        var density = new List<(double, K)>();
        double current = 0;
        double sum = items.Sum(kvp => kvp.Item2);
        foreach (var (k, v) in items)
        {
            current += ((double)v) / sum;
            density.Add((current, k));
        }
        return new DiscreteProbabilityDensity<K>(density);
    }

    public static DiscreteProbabilityDensity<K> Create<K>(IEnumerable<(K, double)> items) where K : notnull
    {
        var density = new List<(double, K)>();
        double current = 0;
        var sum = items.Sum(kvp => kvp.Item2);
        foreach (var (k, v) in items)
        {
            current += ((double)v) / sum;
            density.Add((current, k));
        }
        return new DiscreteProbabilityDensity<K>(density);
    }

    public static DiscreteProbabilityDensity<K> Create<K>(params (K, int)[] items) where K : notnull => Create(items.AsEnumerable());
}

class DiscreteProbabilityDensity<K> where K : notnull
{
    public DiscreteProbabilityDensity(List<(double, K)> density)
    {
        this.density = density;
    }

    private readonly List<(double, K)> density;

    public K Pick(double val)
    {
        var ix = density.BinarySearch((val, default!), Comparer);
        ix = ix < 0 ? ~ix : ix;
        return density[ix].Item2;
    }

    private static WeightComparer Comparer = new WeightComparer();

    class WeightComparer : IComparer<(double, K)>
    {
        public int Compare((double, K) x, (double, K) y)
        {
            return x.Item1.CompareTo(y.Item1);
        }
    }
}



